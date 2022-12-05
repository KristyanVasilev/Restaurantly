namespace Restarauntly.Web.ViewModels.Categories
{
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;

    public class EditCategoryViewModel : DeleteCategoryViewModel, IMapFrom<Category>
    {
    }
}
