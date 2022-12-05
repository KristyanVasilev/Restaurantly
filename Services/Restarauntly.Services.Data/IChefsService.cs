namespace Restarauntly.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Restarauntly.Web.ViewModels.Chefs;

    public interface IChefsService
    {
        Task CreateAsync(CreateChefViewModel input, string userId, string imagePath);

       Task EditAsync(int id, EditChefViewModel input);

        Task DeleteAsync(int id);

        T GetSingleChef<T>(int id);

        IEnumerable<T> GetAll<T>();
    }
}
