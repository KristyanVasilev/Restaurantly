namespace Restarauntly.Web.ViewModels.Booking
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BookTableInputModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 8)]
        public string PhoneNumber { get; set; }

        public DateTime BookingTime { get; set; }

        [Range(1, 14)]
        public int NumberOfPeople { get; set; }

        [StringLength(100)]
        public string Message { get; set; }
    }
}
