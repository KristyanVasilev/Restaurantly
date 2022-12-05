namespace Restarauntly.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Restarauntly.Services.Data;
    using Restarauntly.Web.ViewModels.Chefs;
    using Restarauntly.Web.ViewModels.Events;

    public class InfoController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly IChefsService chefsService;

        public InfoController(IEventsService eventsService, IChefsService chefsService)
        {
            this.eventsService = eventsService;
            this.chefsService = chefsService;
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Chefs()
        {
            var viewModel = new ChefsInListViewModel
            {
                Chefs = this.chefsService.GetAll<ChefViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Events()
        {
            var viewModel = new EventsInListViewModel
            {
                Events = this.eventsService.GetAll<EventViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
