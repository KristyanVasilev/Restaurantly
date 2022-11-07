namespace Restarauntly.Services.Data
{
    using System.Threading.Tasks;

    using Restarauntly.Web.ViewModels.Booking;

    public interface IBookingService
    {
        Task ReserveAsync(BookTableInputModel input);
    }
}
