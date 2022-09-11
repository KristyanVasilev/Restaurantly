namespace Restarauntly.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Restarauntly.Common;
    using Restarauntly.Services.Data;
    using Restarauntly.Web.ViewModels.Dishes;
    using Restarauntly.Web.ViewModels.Tables;
    using System;
    using System.Data;
    using System.Threading.Tasks;

    public class MenuController : BaseController
    {
        private readonly IDishService dishService;
        private readonly ICategoriesService categoriesService;

        public MenuController(IDishService dishService, ICategoriesService categoriesService)
        {
            this.dishService = dishService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Dishes(int id = 1)
        {
            const int itemsPerPage = 10;
            var viewModel = new MenuViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Dishes = this.dishService.GetAll<DishViewModel>(id, itemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult SingleDish(int id)
        {
            var dish = this.dishService.GetSingleDish<SingleDishViewModel>(id);
            dish.Dishes = this.dishService.GetAll<DishViewModel>(id);

            return this.View(dish);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.dishService.DeleteAsync(id);

            this.TempData["Message"] = "Dish deleted successfuly!";
            return this.RedirectToAction("Dishes");
        }

        public IActionResult Edit(int id)
        {
            var viewModel = this.dishService.GetSingleDish<EditDishViewModel>(id);
            viewModel.CategoriesItems = this.categoriesService.GetCategories();
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditDishViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetCategories();
                return this.View(input);
            }

            await this.dishService.EditAsync(id, input);

            this.TempData["Message"] = "Dish edited successfuly!";
            return this.RedirectToAction("Dishes");
        }
    }
}
