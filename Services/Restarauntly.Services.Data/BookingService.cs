namespace Restarauntly.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;
    using Restarauntly.Web.ViewModels.Booking;

    public class BookingService : IBookingService
    {
        private readonly IDeletableEntityRepository<Table> tableRepository;

        public BookingService(IDeletableEntityRepository<Table> tableRepository)
        {
            this.tableRepository = tableRepository;
        }

        public async Task ReserveAsync(BookTableInputModel input)
        {
            var table = this.tableRepository.All()
                .FirstOrDefault(x => x.IsItBooked == false && x.NumberOfSeatingPlaces >= input.NumberOfPeople);

            if (table == null)
            {
                throw new Exception($"No free table found");
            }

            table.IsItBooked = true;

            // TODO: Implement email sender
            await this.tableRepository.SaveChangesAsync();
        }
    }
}
