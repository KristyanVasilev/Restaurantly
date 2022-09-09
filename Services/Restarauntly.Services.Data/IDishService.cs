namespace Restarauntly.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Restarauntly.Web.ViewModels.Dishes;

    public interface IDishService
    {
        IEnumerable<T> GetAll<T>(int pageNumber, int itemsPerPage = 10);

        Task CreateAsync(CreateDishInputModel input, string userId, string imagePath);

        T GetSingleDish<T>(int id);
    }
}
