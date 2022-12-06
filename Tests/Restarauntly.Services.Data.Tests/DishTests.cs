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
    using Restarauntly.Web.ViewModels.Dishes;
    using Xunit;

    public class DishTests
    {
        private readonly Mock<IDeletableEntityRepository<Dish>> mockRepo;
        private readonly DishService service;

        public DishTests()
        {
            this.mockRepo = DishMockRepository.GetDishMockRepo();
            this.service = new DishService(this.mockRepo.Object);
        }

        [Fact]
        public async Task CreateDishTest()
        {
            var dish = new CreateDishInputModel
            {
                Name = "Chocolate suffle",
                CategoryId = 1,
                Ingredients = "Some ingredients",
                Price = 15,
                Quantity = 4,
                Images = new List<IFormFile>(),
            };

            await this.service.CreateAsync(dish, "userID", "dessert");

            var count = this.mockRepo.Object.All().Count();
            var name = this.mockRepo.Object.All().Skip(2).First().Name;
            Assert.Equal("Chocolate suffle", name);

            Assert.True(count == 3);
        }

        [Fact]
        public async Task CreateDishExceptionTest()
        {
            var dish = new CreateDishInputModel
            {
                Name = "Chocolate suffle",
                CategoryId = 1,
                Ingredients = "Some ingredients",
                Price = 15,
                Quantity = 4,
                Images = null,
            };

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.CreateAsync(dish, "userID", "dessert"));
        }

        [Fact]
        public async Task DeleteDishTest()
        {
            await this.service.DeleteAsync(1);
            await this.service.DeleteAsync(2);

            var count = this.mockRepo.Object.All().Count();

            Assert.True(count == 0);
        }

        [Fact]
        public async Task DeleteDishExceptionTest()
        {
            await this.service.DeleteAsync(1);
            await this.service.DeleteAsync(2);

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.DeleteAsync(1));
        }

        [Fact]
        public async Task EditDishTest()
        {
            var dish = new EditDishViewModel
            {
                Name = "Chocolate suffle",
                CategoryId = 1,
                Ingredients = "edited ingredients",
                Price = 150,
                Quantity = 200,
            };

            await this.service.EditAsync(1, dish);
            Assert.Equal("Chocolate suffle", this.mockRepo.Object.All().First().Name);
            Assert.Equal("edited ingredients", this.mockRepo.Object.All().First().Ingredients);
            Assert.Equal(150, this.mockRepo.Object.All().First().Price);
            Assert.Equal(200, this.mockRepo.Object.All().First().Quantity);
        }

        [Fact]
        public async Task EditDishExceptionTest()
        {
            var dish = new EditDishViewModel
            {
                Name = "Chocolate suffle",
                CategoryId = 1,
                Ingredients = "edited ingredients",
                Price = 150,
                Quantity = 200,
            };

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.EditAsync(-1, dish));
        }
    }
}
