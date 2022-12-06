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
    using Restarauntly.Web.ViewModels.Events;

    public class EventsService : IEventsService
    {
        private readonly IDeletableEntityRepository<Event> eventsRepository;

        public EventsService(IDeletableEntityRepository<Event> eventsRepository)
        {
            this.eventsRepository = eventsRepository;
        }

        public async Task CreateAsync(CreateEventInputModel input, string userId, string imagePath)
        {
            var @event = new Event
            {
                Name = input.Name,
                Price = input.Price,
                Description = input.Description,
                CreatedOn = DateTime.UtcNow,
            };

            var allowedExtensions = new[] { "jpg", "png" };
            Directory.CreateDirectory($"{imagePath}/events/");

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

                @event.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/events/{dbImage.Id}.{extension}";

                using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }

            await this.eventsRepository.AddAsync(@event);
            await this.eventsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var @event = this.eventsRepository.All().FirstOrDefault(x => x.Id == id);
            if (@event == null)
            {
                throw new NullReferenceException();
            }

            this.eventsRepository.Delete(@event);

            await this.eventsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, EditEventViewModel input)
        {
            var @event = this.eventsRepository.All().FirstOrDefault(x => x.Id == id);
            if (@event == null)
            {
                throw new NullReferenceException();
            }

            @event.Name = input.Name;
            @event.Price = input.Price;
            @event.Description = input.Description;
            @event.CreatedOn = input.CreatedOn;
            @event.ModifiedOn = DateTime.UtcNow;

            await this.eventsRepository.SaveChangesAsync();
        }

        public T GetSingleEvent<T>(int id)
            => this.eventsRepository
                   .AllAsNoTracking()
                   .Where(x => x.Id == id)
                   .To<T>()
                   .FirstOrDefault();

        public IEnumerable<T> GetAll<T>()
            => this.eventsRepository
                   .AllAsNoTracking()
                   .OrderByDescending(x => x.Id)
                   .To<T>()
                   .ToList();
    }
}
