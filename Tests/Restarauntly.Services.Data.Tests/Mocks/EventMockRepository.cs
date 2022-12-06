namespace Restarauntly.Services.Data.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;

    public class EventMockRepository
    {
        public static Mock<IDeletableEntityRepository<Event>> GetEventMockRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<Event>>();

            var list = new List<Event>()
            {
                new Event
                {
                    Id = 1,
                    Name = "Birthday party",
                    CreatedOn = DateTime.Now,
                    Price = 220,
                    Description = "Some Description",
                },
                new Event
                {
                    Id = 2,
                    Name = "Christmans party",
                    CreatedOn = DateTime.Now,
                    Price = 320,
                    Description = "Some Description",
                },
            };

            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());

            mockRepo.Setup(r => r.AllAsNoTracking()).Returns(list.Where(x => x.IsDeleted == false).AsQueryable());

            mockRepo.Setup(r => r.AllAsNoTrackingWithDeleted()).Returns(list.AsQueryable());

            mockRepo.Setup(r => r.Delete(It.IsAny<Event>()))
                                 .Callback((Event @event) => list.Remove(@event));
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Event>()))
                                 .Callback((Event @event) => list.Add(@event));

            return mockRepo;
        }
    }
}
