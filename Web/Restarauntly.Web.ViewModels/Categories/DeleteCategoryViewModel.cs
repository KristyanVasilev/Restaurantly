namespace Restarauntly.Web.ViewModels.Categories
{
    using System;

    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;

    public class DeleteCategoryViewModel : BaseCategoryViewModel, IMapFrom<Category>
    {
        public int Id { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
