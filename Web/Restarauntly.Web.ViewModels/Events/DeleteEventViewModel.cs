namespace Restarauntly.Web.ViewModels.Events
{
    using System;

    public class DeleteEventViewModel : BaseEventsViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
