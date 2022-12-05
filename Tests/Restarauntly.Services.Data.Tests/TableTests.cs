namespace Restarauntly.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Data.Tests.Mocks;
    using Restarauntly.Web.ViewModels.Tables;
    using Xunit;

    public class TableTests
    {
        private readonly Mock<IDeletableEntityRepository<Table>> mockRepo;
        private readonly TableService service;

        public TableTests()
        {
            this.mockRepo = TableMockRepository.GetTableMockRepo();
            this.service = new TableService(this.mockRepo.Object);
        }

        [Fact]
        public async Task CreateTableTest()
        {
            var table = new CreateTableViewModel
            {
                Id = 3,
                Message = "some message",
                NumberOfSeatingPlaces = 8,
                IsItBooked = false,
            };

            await this.service.CreateAsync(table);

            var count = this.mockRepo.Object.All().Count();
            var message = this.mockRepo.Object.All().First().Message;
            Assert.Equal("Some message", message);

            Assert.True(count == 3);

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.CreateAsync(null));
        }

        [Fact]
        public async Task CreateTableExceptionTest()
        {
            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.CreateAsync(null));
        }

        [Fact]
        public async Task DeleteTableTest()
        {
            await this.service.DeleteAsync(1);
            await this.service.DeleteAsync(2);

            var count = this.mockRepo.Object.All().Count();

            Assert.True(count == 0);
        }

        [Fact]
        public async Task DeleteTableExceptionTest()
        {
            await this.service.DeleteAsync(1);
            await this.service.DeleteAsync(2);

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.DeleteAsync(1));
        }

        [Fact]
        public async Task EditTableTest()
        {
            var table = new EditTableViewModel
            {
                Message = "edited message",
                NumberOfSeatingPlaces = 10,
                IsItBooked = true,
            };

            await this.service.EditAsync(1, table);
            Assert.Equal("edited message", this.mockRepo.Object.All().First().Message);
            Assert.Equal(10, this.mockRepo.Object.All().First().NumberOfSeatingPlaces);
            Assert.True(this.mockRepo.Object.All().First().IsItBooked);
        }

        [Fact]
        public async Task EditTableExceptionTest()
        {
            var table = new EditTableViewModel
            {
                Message = "edited message",
                NumberOfSeatingPlaces = 10,
                IsItBooked = true,
            };

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.EditAsync(-1, table));
            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.EditAsync(1, null));
            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.EditAsync(-1, null));
        }

        [Fact]
        public async Task UnBookTableTest()
        {
            await this.service.UnBookAsync(2);
            Assert.False(this.mockRepo.Object.All().Skip(1).First().IsItBooked);
        }

        [Fact]
        public async Task UnBookTableExceptionTest()
        {
            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.UnBookAsync(-1));
        }
    }
}
