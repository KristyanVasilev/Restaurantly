namespace Restarauntly.Data.Models
{
    using System.Collections.Generic;

    using Restarauntly.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {
        public Ingredient()
        {
            this.Dishes = new HashSet<DishIngredient>();
        }

        public string Name { get; set; }

        public ICollection<DishIngredient> Dishes { get; set; }
    }
}
