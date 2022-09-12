namespace Restarauntly.Web.ViewModels.Tables
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseTableViewModel
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 12)]
        [Display(Name = "#ofPeople")]
        public int NumberOfSeatingPlaces { get; set; }

        [Required]
        public bool IsItBooked { get; set; }

        public string Message { get; set; }
    }
}
