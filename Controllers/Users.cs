using Microsoft.AspNetCore.Mvc;

namespace EventSphereApp.Controllers
{
    public class Users : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
