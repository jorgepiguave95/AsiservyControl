using Microsoft.AspNetCore.Mvc;

namespace ClientGateway.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
