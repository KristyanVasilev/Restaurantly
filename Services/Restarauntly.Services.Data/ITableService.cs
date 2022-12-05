namespace Restarauntly.Services.Data
{
    using System.Threading.Tasks;

    using Restarauntly.Web.ViewModels.Tables;

    public interface ITableService
    {
        Task CreateAsync(CreateTableViewModel input);

        Task EditAsync(int id, EditTableViewModel input);

        Task DeleteAsync(int id);

        Task UnBookAsync(int id, UnBookTableViewModel input);

        T GetSingleTable<T>(int id);
    }
}
