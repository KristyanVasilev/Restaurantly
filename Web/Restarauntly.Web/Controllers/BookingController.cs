namespace Restarauntly.Web.Controllers
{
    using System;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Restarauntly.Services.Data;
    using Restarauntly.Web.ViewModels.Booking;

    public class BookingController : BaseController
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [Authorize]
        public IActionResult Reserve()
        {
            var viewModel = new BookTableInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Reserve(BookTableInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                this.bookingService.ReserveAsync(input);
            }
            catch (Exception ex)
            {
                this.TempData["Message"] = ex.Message;

                // TODO: redirect
                return this.RedirectToAction("Reserve");
            }

            this.TempData["Message"] = "Table booked successfuly!";

            // TODO: redirect
            return this.RedirectToAction("Reserve");
        }
    }
}
