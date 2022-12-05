namespace Restarauntly.Web.ViewModels.Chefs
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class CreateChefViewModel : BaseChefViewModel
    {
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
