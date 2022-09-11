namespace Restarauntly.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Restarauntly.Services.Data;
    using Restarauntly.Web.ViewModels.Dishes;

    public class MenuController : BaseController
    {
        private readonly IDishService dishService;

        public MenuController(IDishService dishService)
        {
            this.dishService = dishService;
        }

        public IActionResult Dishes(int id = 1)
        {
            const int itemsPerPage = 10;
            var viewModel = new MenuViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                //DishCount = this.DishService.GetDishesCount(),
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
    }
}
