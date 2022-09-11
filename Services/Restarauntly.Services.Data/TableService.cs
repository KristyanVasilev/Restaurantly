﻿namespace Restarauntly.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;
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
            this.tableRepository.Delete(table);

            await this.tableRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, EditTableViewModel input)
        {
            var table = this.tableRepository.All().FirstOrDefault(x => x.Id == id);

            table.NumberOfSeatingPlaces = input.NumberOfSeatingPlaces;
            table.Message = input.Message;
            table.IsDeleted = input.IsDeleted;
            table.DeletedOn = input.DeletedOn;
            table.IsItBooked = input.IsItBooked;
            table.BookedTime = input.BookedTime;
            table.ModifiedOn = DateTime.Now;

            await this.tableRepository.SaveChangesAsync();
        }

        public async Task UnBookAsync(int id, UnBookTableViewModel input)
        {
            var table = this.tableRepository.All().FirstOrDefault(x => x.Id == id);

            table.IsItBooked = false;
            await this.tableRepository.SaveChangesAsync();
        }
    }
}
