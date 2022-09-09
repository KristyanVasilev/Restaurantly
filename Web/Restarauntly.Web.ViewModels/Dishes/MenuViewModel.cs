namespace Restarauntly.Web.ViewModels.Dishes
{
    using System.Collections.Generic;

    public class MenuViewModel : PagingViewModel
    {
        public IEnumerable<DishViewModel> Dishes { get; set; }
    }
}
