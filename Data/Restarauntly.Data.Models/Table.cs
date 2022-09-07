namespace Restarauntly.Data.Models
{
    using System;

    using Restarauntly.Data.Common.Models;

    public class Table : BaseDeletableModel<int>
    {
        public int NumberOfSeatingPlaces { get; set; }

        public bool IsItBooked { get; set; }

        public TimeSpan BookedTime { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Message { get; set; }
    }
}
