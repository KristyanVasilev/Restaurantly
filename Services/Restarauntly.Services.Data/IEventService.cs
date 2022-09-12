namespace Restarauntly.Services.Data
{
    using System.Threading.Tasks;

    using Restarauntly.Web.ViewModels.Events;
    using Restarauntly.Web.ViewModels.Tables;

    public interface IEventService
    {
        Task CreateAsync(CreateEventInputModel input, string userId, string imagePath);

        Task EditAsync(int id, EditTableViewModel input);

        Task DeleteAsync(int id);
    }
}
