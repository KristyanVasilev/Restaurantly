namespace Restarauntly.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Restarauntly.Common;
    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Data;
    using Restarauntly.Web.ViewModels.Categories;

    public class CategoriesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly ICategoriesService categoriesService;

        public CategoriesController(IDeletableEntityRepository<Category> categoryRepository, ICategoriesService categoriesService)
        {
            this.categoryRepository = categoryRepository;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.categoryRepository.All().ToListAsync());
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var viewModel = new CreateCategoriesViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateCategoriesViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoriesService.CreateAsync(input);

            this.TempData["Message"] = "Category added successfuly!";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int? id)
        {
            var viewModel = new EditCategoryViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, EditCategoryViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoriesService.EditAsync(id, input);

            this.TempData["Message"] = "Category edited successfuly!";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Delete(int? id)
        {
            var viewModel = new DeleteCategoryViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.categoriesService.DeleteAsync(id);

            this.TempData["Message"] = "Category deleted successfuly!";
            return this.RedirectToAction("Index");
        }
    }
}
