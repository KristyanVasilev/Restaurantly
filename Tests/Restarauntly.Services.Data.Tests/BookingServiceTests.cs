namespace Restarauntly.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Data.Tests.Mocks;
    using Restarauntly.Web.ViewModels.Booking;
    using Xunit;

    public class BookingServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Table>> mockRepo;
        private readonly BookingService service;

        public BookingServiceTests()
        {
            this.mockRepo = TableMockRepository.GetTableMockRepo();
            this.service = new BookingService(this.mockRepo.Object);
        }

        [Fact]
        public async Task ReserveTableTest()
        {
            var table = new BookTableInputModel
            {
                BookingTime = DateTime.Now,
                Email = "Test@gmail.com",
                Message = "Some message",
                Name = "Test",
                NumberOfPeople = 4,
                PhoneNumber = "0000000000",
            };

            await this.service.ReserveAsync(table);

            Assert.True(this.mockRepo.Object.All().First().IsItBooked);
        }

        [Fact]
        public async Task ReserveTableExceptionTest()
        {
            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.ReserveAsync(null));
        }
    }
}
