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
    using Restarauntly.Web.ViewModels.Chefs;

    public class ChefsService : IChefsService
    {
        private readonly IDeletableEntityRepository<Chef> chefsRepository;

        public ChefsService(IDeletableEntityRepository<Chef> chefsRepository)
        {
            this.chefsRepository = chefsRepository;
        }

        public async Task CreateAsync(CreateChefViewModel input, string userId, string imagePath)
        {
            var chef = new Chef
            {
                Name = input.Name,
                JobType = input.JobType,
                CreatedOn = DateTime.UtcNow,
            };

            var allowedExtensions = new[] { "jpg", "png" };
            Directory.CreateDirectory($"{imagePath}/chefs/");

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

                chef.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/chefs/{dbImage.Id}.{extension}";

                using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }

            await this.chefsRepository.AddAsync(chef);
            await this.chefsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chef = this.chefsRepository.All().FirstOrDefault(x => x.Id == id);
            if (chef == null)
            {
                throw new NullReferenceException();
            }

            this.chefsRepository.Delete(chef);

            await this.chefsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, EditChefViewModel input)
        {
            var chef = this.chefsRepository.All().FirstOrDefault(x => x.Id == id);
            if (chef == null)
            {
                throw new NullReferenceException();
            }

            chef.Name = input.Name;
            chef.JobType = input.JobType;
            chef.CreatedOn = input.CreatedOn;
            chef.ModifiedOn = DateTime.UtcNow;

            await this.chefsRepository.SaveChangesAsync();
        }

        public T GetSingleChef<T>(int id)
            => this.chefsRepository
                   .AllAsNoTracking()
                   .Where(x => x.Id == id)
                   .To<T>()
                   .FirstOrDefault();

        public IEnumerable<T> GetAll<T>()
            => this.chefsRepository
                   .AllAsNoTracking()
                   .OrderByDescending(x => x.Id)
                   .To<T>()
                   .ToList();
    }
}
