namespace Restarauntly.Web.ViewModels.Tables
{
    using System;

    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;

    public class UnBookTableViewModel : IMapFrom<Table>
    {
        public int Id { get; set; }

        public bool IsItBook { get; set; }

        public DateTime BookedTime { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
