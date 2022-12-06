namespace Restarauntly.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Data.Tests.Mocks;
    using Restarauntly.Web.ViewModels.Categories;
    using Xunit;

    public class CategoriesServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Category>> mockRepo;
        private readonly CategoriesService service;

        public CategoriesServiceTest()
        {
            this.mockRepo = CategoryMockRepository.GetCategoryMockRepo();
            this.service = new CategoriesService(this.mockRepo.Object);
        }

        [Fact]
        public async Task CreateCategoryTest()
        {
            var category = new CreateCategoriesViewModel
            {
                Name = "Foo",
            };

            await this.service.CreateAsync(category);

            var count = this.mockRepo.Object.All().Count();
            var name = this.mockRepo.Object.All().Skip(2).First().Name;
            Assert.Equal("Foo", name);

            Assert.True(count == 3);
        }

        [Fact]
        public async Task CreateCategoryExceptionTest()
        {
            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.CreateAsync(null));
        }

        [Fact]
        public async Task DeleteCategoryTest()
        {
            await this.service.DeleteAsync(1);
            await this.service.DeleteAsync(2);

            var count = this.mockRepo.Object.All().Count();

            Assert.True(count == 0);
        }

        [Fact]
        public async Task DeleteCategroyExceptionTest()
        {
            await this.service.DeleteAsync(1);
            await this.service.DeleteAsync(2);

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.DeleteAsync(1));
        }

        [Fact]
        public async Task EditCategoryTest()
        {
            var category = new EditCategoryViewModel
            {
                Id = 1,
                Name = "Foo",
            };

            await this.service.EditAsync(1, category);
            Assert.Equal("Foo", this.mockRepo.Object.All().First().Name);
        }

        [Fact]
        public async Task EditCategoryExceptionTest()
        {
            var category = new EditCategoryViewModel
            {
                Id = 1,
                Name = "Foo",
            };

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.EditAsync(-1, category));
        }
    }
}
