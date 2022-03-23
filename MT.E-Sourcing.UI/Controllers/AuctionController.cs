using Microsoft.AspNetCore.Mvc;
using MT.E_Sourcing.UI.ViewModel;
using MT.E_Sourcing.WebApp.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.E_Sourcing.UI.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuctionController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userList = await _userRepository.GetAllAsync();
            ViewBag.UserList = userList;
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
