namespace Restarauntly.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Mapping;
    using Restarauntly.Web.ViewModels.Tables;

    public class TableService : ITableService
    {
        private readonly IDeletableEntityRepository<Table> tableRepository;

        public TableService(IDeletableEntityRepository<Table> tableRepository)
        {
            this.tableRepository = tableRepository;
        }

        public async Task CreateAsync(CreateTableViewModel input)
        {
            if (input == null)
            {
                throw new NullReferenceException();
            }

            var table = new Table
            {
                NumberOfSeatingPlaces = input.NumberOfSeatingPlaces,
                IsItBooked = input.IsItBooked,
                Message = input.Message,
                CreatedOn = DateTime.Now,
            };

            await this.tableRepository.AddAsync(table);
            await this.tableRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var table = this.tableRepository.All().FirstOrDefault(x => x.Id == id);
            if (table == null)
            {
                throw new NullReferenceException();
            }

            this.tableRepository.Delete(table);

            await this.tableRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, EditTableViewModel input)
        {
            var table = this.tableRepository.All().FirstOrDefault(x => x.Id == id);
            if (table == null || input == null)
            {
                throw new NullReferenceException();
            }

            table.NumberOfSeatingPlaces = input.NumberOfSeatingPlaces;
            table.Message = input.Message;
            table.IsDeleted = input.IsDeleted;
            table.DeletedOn = input.DeletedOn;
            table.IsItBooked = input.IsItBooked;
            table.BookedTime = input.BookedTime;
            table.ModifiedOn = DateTime.Now;

            await this.tableRepository.SaveChangesAsync();
        }

        public async Task UnBookAsync(int id)
        {
            var table = this.tableRepository.All().FirstOrDefault(x => x.Id == id);
            if (table == null)
            {
                throw new NullReferenceException();
            }

            table.IsItBooked = false;
            await this.tableRepository.SaveChangesAsync();
        }

        public T GetSingleTable<T>(int id)
             => this.tableRepository
             .AllAsNoTracking()
             .Where(x => x.Id == id)
             .To<T>()
             .FirstOrDefault();
    }
}
