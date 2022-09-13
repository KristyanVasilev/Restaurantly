namespace Restarauntly.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Restarauntly.Services.Data;
    using Restarauntly.Web.ViewModels.Events;

    public class InfoController : Controller
    {
        private readonly IEventsService eventsService;

        public InfoController(IEventsService eventsService)
        {
            this.eventsService = eventsService;
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Chefs()
        {
            return this.View();
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
