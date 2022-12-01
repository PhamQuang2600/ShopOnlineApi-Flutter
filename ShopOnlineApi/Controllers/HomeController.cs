using Microsoft.AspNetCore.Mvc;

namespace ShopOnlineApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
