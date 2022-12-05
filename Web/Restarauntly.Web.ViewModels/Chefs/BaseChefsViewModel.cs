namespace Restarauntly.Web.ViewModels.Chefs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseChefViewModel
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string JobType { get; set; }
    }
}
