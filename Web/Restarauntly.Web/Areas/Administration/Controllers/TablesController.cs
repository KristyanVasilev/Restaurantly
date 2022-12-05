namespace Restarauntly.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Restarauntly.Common;
    using Restarauntly.Data.Common.Repositories;
    using Restarauntly.Data.Models;
    using Restarauntly.Services.Data;
    using Restarauntly.Web.ViewModels.Tables;

    [Area("Administration")]
    public class TablesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Table> tableRepository;
        private readonly ITableService tableService;

        public TablesController(IDeletableEntityRepository<Table> tableRepository, ITableService tableService)
        {
            this.tableRepository = tableRepository;
            this.tableService = tableService;
        }

        public async Task<IActionResult> Index()
        {
              return this.View(await this.tableRepository.All().ToListAsync());
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var viewModel = new CreateTableViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateTableViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.tableService.CreateAsync(input);

            this.TempData["Message"] = "Table added successfuly!";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var viewModel = this.tableService.GetSingleTable<EditTableViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, EditTableViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.tableService.EditAsync(id, input);

            this.TempData["Message"] = "Table edited successfuly!";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var viewModel = this.tableService.GetSingleTable<DeleteTableViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.tableService.DeleteAsync(id);

            this.TempData["Message"] = "Table deleted successfuly!";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult UnBook(int id)
        {
            var viewModel = new UnBookTableViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [ActionName("UnBook")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> UnBookConfirmation(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            try
            {
                await this.tableService.UnBookAsync(id);
                this.TempData["Message"] = "Table unbooked successfuly!";
            }
            catch (System.Exception)
            {
                this.TempData["Message"] = "Table is already unbooked or wrong Id!";
            }

            return this.RedirectToAction("Index");
        }
    }
}
