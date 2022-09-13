namespace Restarauntly.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Restarauntly.Web.ViewModels.Events;

    public interface IEventsService
    {
        Task CreateAsync(CreateEventInputModel input, string userId, string imagePath);

        Task EditAsync(int id, EditEventViewModel input);

        Task DeleteAsync(int id);

        T GetSingleEvent<T>(int id);

        IEnumerable<T> GetAll<T>();
    }
}
