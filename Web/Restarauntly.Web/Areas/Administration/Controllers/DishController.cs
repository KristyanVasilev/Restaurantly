namespace Restarauntly.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Restarauntly.Common;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Data;
    using Restarauntly.Web.ViewModels.Dishes;

    public class DishController : AdministrationController
    {
        private readonly IDishService dishService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;
        private readonly IWebHostEnvironment environment;

        public DishController(
            IDishService dishService,
            UserManager<ApplicationUser> userManager,
            ICategoriesService categoriesService,
            IWebHostEnvironment environment)
        {
            this.dishService = dishService;
            this.userManager = userManager;
            this.categoriesService = categoriesService;
            this.environment = environment;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var viewModel = new CreateDishInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetCategories();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateDishInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetCategories();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.dishService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Dish added successfuly!";
            return this.RedirectToAction("Create"); //TODO: redirect to dish
        }
    }
}
