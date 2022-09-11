namespace Restarauntly.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class InfoController : Controller
    {
        public IActionResult Contact()
        {
            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }
    }
}
