namespace Restarauntly.Web.ViewModels.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEventsViewModel
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [Range(1, 10000)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 40)]
        public string Description { get; set; }
    }
}
