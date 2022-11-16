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
    using Restarauntly.Data;
    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Data;
    using Restarauntly.Web.ViewModels.Events;

    [Area("Administration")]
    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IDeletableEntityRepository<Event> eventRepository;

        public EventsController(IEventsService eventsService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IDeletableEntityRepository<Event> eventRepository)
        {
            this.eventsService = eventsService;
            this.userManager = userManager;
            this.environment = environment;
            this.eventRepository = eventRepository;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.eventRepository.All().ToListAsync());
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var viewModel = new CreateEventInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateEventInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.eventsService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Event added successfuly!";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public ActionResult Edit(int id)
        {
            var viewModel = this.eventsService.GetSingleEvent<EditEventViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, EditEventViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.eventsService.EditAsync(id, input);

            this.TempData["Message"] = "Event edited successfuly!";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var viewModel = new DeleteEventViewModel();
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

            await this.eventsService.DeleteAsync(id);

            this.TempData["Message"] = "Event deleted successfuly!";
            return this.RedirectToAction("Index");
        }
    }
}
