namespace Restarauntly.Web.ViewModels.Events
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class CreateEventInputModel : BaseEventsViewModel
    {
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
