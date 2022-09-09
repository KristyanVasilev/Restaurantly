namespace Restarauntly.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Restarauntly.Data;
    using Restarauntly.Data.Models;

    public class CategoriesController : AdministrationController
    {
        private readonly ApplicationDbContext db;

        public CategoriesController(ApplicationDbContext context)
        {
            this.db = context;
        }

        // GET: Administration/Categories
        public async Task<IActionResult> Index()
        {
              return this.View(await this.db.Categories.ToListAsync());
        }

        // GET: Administration/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.db.Categories == null)
            {
                return this.NotFound();
            }

            var category = await this.db.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        // GET: Administration/Categories/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Category category)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Add(category);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(category);
        }

        // GET: Administration/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.db.Categories == null)
            {
                return this.NotFound();
            }

            var category = await this.db.Categories.FindAsync(id);
            if (category == null)
            {
                return this.NotFound();
            }
            return this.View(category);
        }

        // POST: Administration/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Category category)
        {
            if (id != category.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.db.Update(category);
                    await this.db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CategoryExists(category.Id))
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

            return this.View(category);
        }

        // GET: Administration/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.db.Categories == null)
            {
                return this.NotFound();
            }

            var category = await this.db.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        // POST: Administration/Categories/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.db.Categories == null)
            {
                return this.Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
            }

            var category = await this.db.Categories.FindAsync(id);
            if (category != null)
            {
                this.db.Categories.Remove(category);
            }

            await this.db.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CategoryExists(int id)
        {
          return this.db.Categories.Any(e => e.Id == id);
        }
    }
}
