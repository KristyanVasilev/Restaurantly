namespace Restarauntly.Web.ViewModels.Tables
{
    using System;

    public class DeleteTableViewModel : EditTableViewModel
    {
        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
