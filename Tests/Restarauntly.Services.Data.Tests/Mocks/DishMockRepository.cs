namespace Restarauntly.Services.Data.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;

    public class DishMockRepository
    {
        public static Mock<IDeletableEntityRepository<Dish>> GetDishMockRepo()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<Dish>>();

            var list = new List<Dish>()
            {
                new Dish
                {
                    Id = 1,
                    Name = "Lobster",
                    CreatedOn = DateTime.Now,
                    Category = new Category { Name = "Dessert" },
                    Ingredients = "Some ingredients",
                    Price = 12,
                    Quantity = 1,
                },
                new Dish
                {
                    Id = 2,
                    Name = "Cake",
                    CreatedOn = DateTime.Now,
                    Category = new Category { Name = "Dessert" },
                    Ingredients = "Some ingredients",
                    Price = 5,
                    Quantity = 3,
                },
            };

            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());

            mockRepo.Setup(r => r.AllAsNoTracking()).Returns(list.Where(x => x.IsDeleted == false).AsQueryable());

            mockRepo.Setup(r => r.AllAsNoTrackingWithDeleted()).Returns(list.AsQueryable());

            mockRepo.Setup(r => r.Delete(It.IsAny<Dish>()))
                                 .Callback((Dish dish) => list.Remove(dish));
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Dish>()))
                                 .Callback((Dish dish) => list.Add(dish));

            return mockRepo;
        }
    }
}
