namespace Restarauntly.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;
    using Restarauntly.Web.ViewModels.Categories;
    using Restarauntly.Web.ViewModels.Tables;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(CreateCategoriesViewModel input)
        {
            var category = new Category
            {
                Name = input.Name,
                CreatedOn = input.CreatedOn,
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = this.categoryRepository.All().FirstOrDefault(x => x.Id == id);
            this.categoryRepository.Delete(category);

            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, EditCategoryViewModel input)
        {
            var category = this.categoryRepository.All().FirstOrDefault(x => x.Id == id);

            category.Name = input.Name;
            category.CreatedOn = input.CreatedOn;
            category.ModifiedOn = input.ModifiedOn;

            await this.categoryRepository.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GetCategories()
        {
            return this.categoryRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            })
            .OrderBy(x => x.Name)
            .ToList()
            .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public T GetSingleCategory<T>(int id)
            => this.categoryRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefault();
    }
}
