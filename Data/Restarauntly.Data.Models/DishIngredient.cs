namespace Restarauntly.Data.Models
{
    public class DishIngredient
    {
        public int Id { get; set; }

        public int DishId { get; set; }

        public virtual Dish Dish { get; set; }

        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
    }
}
