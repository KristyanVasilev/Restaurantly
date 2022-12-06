namespace Restarauntly.Services.Data.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;

    public class CategoryMockRepository
    {
        public static Mock<IDeletableEntityRepository<Category>> GetCategoryMockRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();

            var list = new List<Category>()
            {
                new Category
                {
                    Id = 1,
                    Name = "Dessert",
                    CreatedOn = DateTime.Now,
                },
                new Category
                {
                    Id = 2,
                    Name = "Starters",
                    CreatedOn = DateTime.Now,
                },
            };

            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());

            mockRepo.Setup(r => r.AllAsNoTracking()).Returns(list.Where(x => x.IsDeleted == false).AsQueryable());

            mockRepo.Setup(r => r.AllAsNoTrackingWithDeleted()).Returns(list.AsQueryable());

            mockRepo.Setup(r => r.Delete(It.IsAny<Category>()))
                                 .Callback((Category category) => list.Remove(category));
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Category>()))
                                 .Callback((Category category) => list.Add(category));

            return mockRepo;
        }
    }
}
