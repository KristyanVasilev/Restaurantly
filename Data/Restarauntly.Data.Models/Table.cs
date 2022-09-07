namespace Restarauntly.Data.Models
{
    using System;

    using Restarauntly.Data.Common.Models;

    public class Table : BaseDeletableModel<int>
    {
        public int NumberOfSeatingPlaces { get; set; }

        public bool IsItBooked { get; set; }

        public DateTime? BookedTime { get; set; }

        public string Message { get; set; }
    }
}
