namespace Restarauntly.Services.Data
{
    using System.Threading.Tasks;

    using Restarauntly.Web.ViewModels.Booking;
    using Restarauntly.Web.ViewModels.Dishes;

    public interface IBookingService
    {
        Task ReserveAsync(BookTableInputModel input);
    }
}
