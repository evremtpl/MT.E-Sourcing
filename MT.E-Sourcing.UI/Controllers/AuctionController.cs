using Microsoft.AspNetCore.Mvc;
using MT.E_Sourcing.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.E_Sourcing.UI.Controllers
{
    public class AuctionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AuctionViewModel model)
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
