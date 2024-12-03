using Microsoft.AspNetCore.Mvc;

namespace WebSterlingTea.Controllers
{
    public class AaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
