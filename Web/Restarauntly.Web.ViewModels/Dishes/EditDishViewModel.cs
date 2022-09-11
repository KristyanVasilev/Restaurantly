namespace Restarauntly.Web.ViewModels.Dishes
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;

    public class EditDishViewModel : BaseDishViewModel, IMapFrom<Dish>
    {
        public int Id { get; set; }
    }
}
