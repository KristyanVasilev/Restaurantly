namespace Restarauntly.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
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
        public async Task<IActionResult> Reserve(BookTableInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.bookingService.ReserveAsync(input);
                this.TempData["Message"] = "Table booked successfuly!";
            }
            catch (System.Exception)
            {
                this.TempData["Message"] = "There are no free tables at the moment! " +
                    "Try again later or call 0897 777 777";
            }

            return this.RedirectToAction("Reserve");
        }
    }
}
