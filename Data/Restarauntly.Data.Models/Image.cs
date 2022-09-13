namespace Restarauntly.Data.Models
{
    using System;

    using Restarauntly.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int? DishId { get; set; }

        public virtual Dish Dish { get; set; }

        public int? EventId { get; set; }

        public virtual Event Event { get; set; }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
