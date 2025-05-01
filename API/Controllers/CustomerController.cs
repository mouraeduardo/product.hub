using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
