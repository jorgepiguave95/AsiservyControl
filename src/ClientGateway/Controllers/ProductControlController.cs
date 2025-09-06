using Microsoft.AspNetCore.Mvc;

namespace ClientGateway.Controllers
{
    public class ProductControlController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
