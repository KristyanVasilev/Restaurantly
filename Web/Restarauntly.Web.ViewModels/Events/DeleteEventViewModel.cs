namespace Restarauntly.Web.ViewModels.Events
{
    using System;

    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;

    public class DeleteEventViewModel : BaseEventsViewModel, IMapFrom<Event>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
