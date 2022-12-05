namespace Restarauntly.Web.Controllers
{
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Restarauntly.Services.Data;
    using Restarauntly.Services.Messaging;
    using Restarauntly.Web.ViewModels.Booking;

    public class BookingController : BaseController
    {
        private readonly IBookingService bookingService;
        private readonly IEmailSender emailSender;

        public BookingController(IBookingService bookingService, IEmailSender emailSender)
        {
            this.bookingService = bookingService;
            this.emailSender = emailSender;
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
                var html = new StringBuilder();
                html.AppendLine($"<h1> Hello {input.Name}!<h1>");
                html.AppendLine($"<h2>Thank you for booking table for {input.NumberOfPeople}.<h2>");
                html.AppendLine($"<h3> Booked for: {input.BookingTime}.<h3>");
                html.AppendLine($"<h3> Booked for: {input.BookingTime}.<h3>");
                html.AppendLine($"<h3> For additional information call us: 0895 413 777<h3>");

                await this.emailSender.SendEmailAsync("Restaurantly@gmail.com", "Restaurantly", $"{input.Email}", "Successfuly booked a table", html.ToString());
                this.TempData["Message"] = "Table booked successfuly!";
            }
            catch (System.Exception)
            {
                this.TempData["Message"] = "There are no free tables at the moment! Try again later or call 0895 413 777";
            }

            return this.RedirectToAction("Reserve");
        }
    }
}
