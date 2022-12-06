namespace Restarauntly.Services.Data.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;

    public class ChefMockRepository
    {
        public static Mock<IDeletableEntityRepository<Chef>> GetChefMockRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<Chef>>();

            var list = new List<Chef>()
            {
                new Chef
                {
                    Id = 1,
                    Name = "Gordon Ramsey",
                    CreatedOn = DateTime.Now,
                    JobType = "Master chef",
                },
                new Chef
                {
                    Id = 2,
                    Name = "Jamie Oliver",
                    CreatedOn = DateTime.Now,
                    JobType = "Master chef",
                },
            };

            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());

            mockRepo.Setup(r => r.AllAsNoTracking()).Returns(list.Where(x => x.IsDeleted == false).AsQueryable());

            mockRepo.Setup(r => r.AllAsNoTrackingWithDeleted()).Returns(list.AsQueryable());

            mockRepo.Setup(r => r.Delete(It.IsAny<Chef>()))
                                 .Callback((Chef chef) => list.Remove(chef));
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Chef>()))
                                 .Callback((Chef chef) => list.Add(chef));

            return mockRepo;
        }
    }
}
