using Microsoft.AspNetCore.Mvc;
using MT.E_Sourcing.UI.ViewModel;

namespace MT.E_Sourcing.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            return View();
        }
    }
}
