namespace Restarauntly.Web.ViewModels.Tables
{
    using System;

    public class EditTableViewModel : BaseTableViewModel
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? BookedTime { get; set; }
    }
}
