namespace Restarauntly.Services.Data.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;

    public class TableMockRepository
    {
        public static Mock<IDeletableEntityRepository<Table>> GetTableMockRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<Table>>();

            var list = new List<Table>()
            {
                new Table
                {
                    Id = 1,
                    CreatedOn = DateTime.Now,
                    Message = "Some message",
                    NumberOfSeatingPlaces = 4,
                    IsItBooked = false,
                },
                new Table
                {
                    Id = 2,
                    CreatedOn = DateTime.Now,
                    Message = "Some other message",
                    NumberOfSeatingPlaces = 10,
                    IsItBooked = true,
                },
            };

            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());

            mockRepo.Setup(r => r.AllAsNoTracking()).Returns(list.Where(x => x.IsDeleted == false).AsQueryable());

            mockRepo.Setup(r => r.AllAsNoTrackingWithDeleted()).Returns(list.AsQueryable());

            mockRepo.Setup(r => r.Delete(It.IsAny<Table>()))
                                 .Callback((Table table) => list.Remove(table));
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Table>()))
                                 .Callback((Table table) => list.Add(table));

            return mockRepo;
        }
    }
}
