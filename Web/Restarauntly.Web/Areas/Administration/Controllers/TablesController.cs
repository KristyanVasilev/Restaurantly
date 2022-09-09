using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restarauntly.Data;
using Restarauntly.Data.Common.Repositories;
using Restarauntly.Data.Models;

namespace Restarauntly.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class TablesController : Controller
    {
        private readonly IDeletableEntityRepository<Table> tableRepository;

        public TablesController(IDeletableEntityRepository<Table> tableRepository)
        {
            this.tableRepository = tableRepository;
        }

        // GET: Administration/Tables
        public async Task<IActionResult> Index()
        {
              return this.View(await this.tableRepository.All().ToListAsync());
        }

        // GET: Administration/Tables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.tableRepository.All() == null)
            {
                return this.NotFound();
            }

            var table = await this.tableRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (table == null)
            {
                return this.NotFound();
            }

            return this.View(table);
        }

        // GET: Administration/Tables/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Tables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumberOfSeatingPlaces,IsItBooked,BookedTime,Message,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Table table)
        {
            if (this.ModelState.IsValid)
            {
                await this.tableRepository.AddAsync(table);
                await this.tableRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(table);
        }

        // GET: Administration/Tables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.tableRepository.All() == null)
            {
                return this.NotFound();
            }

            var table = this.tableRepository.All().FirstOrDefault(x => x.Id == id);
            if (table == null)
            {
                return this.NotFound();
            }

            return this.View(table);
        }

        // POST: Administration/Tables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumberOfSeatingPlaces,IsItBooked,BookedTime,Message,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Table table)
        {
            if (id != table.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.tableRepository.Update(table);
                    await this.tableRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.TableExists(table.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(table);
        }

        // GET: Administration/Tables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.tableRepository.All() == null)
            {
                return this.NotFound();
            }

            var table = await this.tableRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (table == null)
            {
                return this.NotFound();
            }

            return this.View(table);
        }

        // POST: Administration/Tables/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.tableRepository.All() == null)
            {
                return this.Problem("Entity set 'ApplicationDbContext.Tables'  is null.");
            }

            var table = this.tableRepository.All().FirstOrDefault(x => x.Id == id);
            if (table != null)
            {
                this.tableRepository.Delete(table);
            }

            await this.tableRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool TableExists(int id)
        {
          return this.tableRepository.All().Any(e => e.Id == id);
        }
    }
}
