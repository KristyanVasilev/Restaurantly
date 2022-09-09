namespace Restarauntly.Web.ViewModels.Dishes
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class CreateDishInputModel : BaseDishViewModel
    {
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
