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
    using Restarauntly.Web.ViewModels.Chefs;
    using Xunit;

    public class ChefTests
    {
        private readonly Mock<IDeletableEntityRepository<Chef>> mockRepo;
        private readonly ChefsService service;

        public ChefTests()
        {
            this.mockRepo = ChefMockRepository.GetChefMockRepo();
            this.service = new ChefsService(this.mockRepo.Object);
        }

        [Fact]
        public async Task CreateChefTest()
        {
            var chef = new CreateChefViewModel
            {
                Name = "Saki",
                JobType = "Sushi chef",
                Images = new List<IFormFile>(),
            };

            await this.service.CreateAsync(chef, "userID", "dessert");

            var count = this.mockRepo.Object.All().Count();
            var name = this.mockRepo.Object.All().Skip(2).First().Name;
            Assert.Equal("Saki", name);

            Assert.True(count == 3);
        }

        [Fact]
        public async Task CreateChefExceptionTest()
        {
            var chef = new CreateChefViewModel
            {
                Name = "Saki",
                JobType = "Sushi chef",
                Images = null,
            };

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.CreateAsync(chef, "userID", "dessert"));
        }

        [Fact]
        public async Task DeleteChefTest()
        {
            await this.service.DeleteAsync(1);
            await this.service.DeleteAsync(2);

            var count = this.mockRepo.Object.All().Count();

            Assert.True(count == 0);
        }

        [Fact]
        public async Task DeleteChefExceptionTest()
        {
            await this.service.DeleteAsync(1);
            await this.service.DeleteAsync(2);

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.DeleteAsync(1));
        }

        [Fact]
        public async Task EditChefhTest()
        {
            var chef = new EditChefViewModel
            {
                Id = 1,
                Name = "Saki",
                JobType = "Sushi chef",
            };

            await this.service.EditAsync(1, chef);
            Assert.Equal("Saki", this.mockRepo.Object.All().First().Name);
            Assert.Equal("Sushi chef", this.mockRepo.Object.All().First().JobType);
        }

        [Fact]
        public async Task EditChefExceptionTest()
        {
            var chef = new EditChefViewModel
            {
                Id = 1,
                Name = "Saki",
                JobType = "Sushi chef",
            };

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.EditAsync(-1, chef));
        }
    }
}
