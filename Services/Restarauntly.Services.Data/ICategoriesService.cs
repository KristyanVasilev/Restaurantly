namespace Restarauntly.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Restarauntly.Web.ViewModels.Categories;
    using Restarauntly.Web.ViewModels.Tables;

    public interface ICategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetCategories();

        Task CreateAsync(CreateCategoriesViewModel input);

        Task EditAsync(int id, EditCategoryViewModel input);

        Task DeleteAsync(int id);
    }
}
