namespace Restarauntly.Web.ViewModels.Tables
{
    using System;

    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;

    public class EditTableViewModel : BaseTableViewModel, IMapFrom<Table>
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? BookedTime { get; set; }
    }
}
