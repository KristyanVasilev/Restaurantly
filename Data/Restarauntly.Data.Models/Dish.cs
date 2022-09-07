namespace Restarauntly.Data.Models
{
    using System.Collections.Generic;

    using Restarauntly.Data.Common.Models;

    public class Dish : BaseDeletableModel<int>
    {
        public Dish()
        {
            this.Ingredients = new HashSet<DishIngredient>();
            this.Images = new HashSet<Image>();
        }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<DishIngredient> Ingredients { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        // TODO: Vote prop
    }
}
