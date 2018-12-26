using Microsoft.AspNetCore.Mvc;

namespace QuartzApiCore.API.Controllers
{
    public class TriggersController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}