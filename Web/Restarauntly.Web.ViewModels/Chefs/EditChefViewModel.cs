namespace Restarauntly.Web.ViewModels.Chefs
{
    using System;

    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;

    public class EditChefViewModel : BaseChefViewModel, IMapFrom<Chef>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
