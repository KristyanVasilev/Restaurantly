namespace Restarauntly.Data.Models
{
    using System.Collections.Generic;

    using Restarauntly.Data.Common.Models;

    public class Chef : BaseDeletableModel<int>
    {
        public Chef()
        {
            this.Images = new HashSet<Image>();
        }

        public string Name { get; set; }

        public string JobType { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
