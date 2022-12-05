namespace Restarauntly.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Restarauntly.Common;
    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Data;
    using Restarauntly.Web.ViewModels.Chefs;
    using Restarauntly.Web.ViewModels.Events;

    public class ChefsController : AdministrationController
    {
        private readonly IChefsService chefsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IDeletableEntityRepository<Chef> chefRepository;

        public ChefsController(
            IChefsService chefsService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IDeletableEntityRepository<Chef> chefRepository)
        {
            this.chefsService = chefsService;
            this.userManager = userManager;
            this.environment = environment;
            this.chefRepository = chefRepository;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.chefRepository.All().ToListAsync());
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var viewModel = new CreateChefViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateChefViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.chefsService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Chef added successfuly!";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var viewModel = this.chefsService.GetSingleChef<DeleteChefViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.chefsService.DeleteAsync(id);

            this.TempData["Message"] = "Chef deleted successfuly!";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public ActionResult Edit(int id)
        {
            var viewModel = this.chefsService.GetSingleChef<EditChefViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, EditChefViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.chefsService.EditAsync(id, input);

            this.TempData["Message"] = "Chef edited successfuly!";
            return this.RedirectToAction("Index");
        }
    }
}
