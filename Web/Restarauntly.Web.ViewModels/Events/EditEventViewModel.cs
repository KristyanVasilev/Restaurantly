namespace Restarauntly.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;

    public class EditEventViewModel : BaseEventsViewModel, IMapFrom<Event>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
