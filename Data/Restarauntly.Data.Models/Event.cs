namespace Restarauntly.Data.Models
{
    using System.Collections.Generic;

    using Restarauntly.Data.Common.Models;

    public class Event : BaseDeletableModel<int>
    {
        public Event()
        {
            this.Images = new HashSet<Image>();
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int MyProperty { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
