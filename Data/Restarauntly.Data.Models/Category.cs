namespace Restarauntly.Data.Models
{
    using System.Collections.Generic;

    using Restarauntly.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Dishes = new HashSet<Dish>();
        }

        public string Name { get; set; }

        public ICollection<Dish> Dishes { get; set; }
    }
}
