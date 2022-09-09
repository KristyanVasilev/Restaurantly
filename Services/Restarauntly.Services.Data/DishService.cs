namespace Restarauntly.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;
    using Restarauntly.Web.ViewModels.Dishes;

    public class DishService : IDishService
    {
        private readonly IDeletableEntityRepository<Dish> dishRepository;

        public DishService(IDeletableEntityRepository<Dish> dishRepository)
        {
            this.dishRepository = dishRepository;
        }

        public async Task CreateAsync(CreateDishInputModel input, string userId, string imagePath)
        {
            var dish = new Dish
            {
                Name = input.Name,
                CategoryId = input.CategoryId,
                Ingredients = input.Ingredients,
                Price = input.Price,
                Quantity = input.Quantity,
                UserId = userId,
            };

            var allowedExtensions = new[] { "jpg", "png" };
            Directory.CreateDirectory($"{imagePath}/dishes/");

            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');

                if (!allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    UserId = userId,
                    Extension = extension,
                };

                dish.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/dishes/{dbImage.Id}.{extension}";

                using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }

            await this.dishRepository.AddAsync(dish);
            await this.dishRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int pageNumber, int itemsPerPage = 10)
            => this.dishRepository
                   .AllAsNoTracking()
                   .OrderByDescending(x => x.Id)
                   .Skip((pageNumber - 1) * itemsPerPage)
                   .Take(itemsPerPage)
                   .To<T>()
                   .ToList();

        public T GetSingleDish<T>(int id)
          => this.dishRepository
                 .AllAsNoTracking()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefault();
    }
}
