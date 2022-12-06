namespace Restarauntly.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Moq;
    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Data.Tests.Mocks;
    using Restarauntly.Web.ViewModels.Events;
    using Xunit;

    public class EventServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Event>> mockRepo;
        private readonly EventsService service;

        public EventServiceTest()
        {
            this.mockRepo = EventMockRepository.GetEventMockRepo();
            this.service = new EventsService(this.mockRepo.Object);
        }

        [Fact]
        public async Task CreateEventTest()
        {
            var @event = new CreateEventInputModel
            {
                Name = "Private party",
                Price = 320,
                Description = "Some Description",
                Images = new List<IFormFile>(),
            };

            await this.service.CreateAsync(@event, "userID", "event");

            var count = this.mockRepo.Object.All().Count();
            var name = this.mockRepo.Object.All().Skip(2).First().Name;
            Assert.Equal("Private party", name);

            Assert.True(count == 3);
        }

        [Fact]
        public async Task CreateEventExceptionTest()
        {
            var @event = new CreateEventInputModel
            {
                Name = "Private party",
                Price = 320,
                Description = "Some Description",
                Images = null,
            };

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.CreateAsync(@event, "userID", "event"));
        }

        [Fact]
        public async Task DeleteEventTest()
        {
            await this.service.DeleteAsync(1);
            await this.service.DeleteAsync(2);

            var count = this.mockRepo.Object.All().Count();

            Assert.True(count == 0);
        }

        [Fact]
        public async Task DeleteEventExceptionTest()
        {
            await this.service.DeleteAsync(1);
            await this.service.DeleteAsync(2);

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.DeleteAsync(1));
        }

        [Fact]
        public async Task EditEventTest()
        {
            var @event = new EditEventViewModel
            {
                Name = "Private party",
                Price = 520,
                Description = "Edited Description",
            };

            await this.service.EditAsync(1, @event);
            Assert.Equal("Private party", this.mockRepo.Object.All().First().Name);
            Assert.Equal(520, this.mockRepo.Object.All().First().Price);
            Assert.Equal("Edited Description", this.mockRepo.Object.All().First().Description);
        }

        [Fact]
        public async Task EditEventExceptionTest()
        {
            var @event = new EditEventViewModel
            {
                Name = "Private party",
                Price = 520,
                Description = "Edited Description",
            };

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.EditAsync(-1, @event));
        }
    }
}
