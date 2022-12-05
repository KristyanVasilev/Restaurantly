namespace Restarauntly.Web.ViewModels.Tables
{
    using System;

    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;

    public class DeleteTableViewModel : EditTableViewModel, IMapFrom<Table>
    {
        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
