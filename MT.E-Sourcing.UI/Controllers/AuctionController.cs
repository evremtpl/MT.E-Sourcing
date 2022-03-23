using Microsoft.AspNetCore.Mvc;
using MT.E_Sourcing.UI.ViewModel;
using MT.E_Sourcing.WebApp.Core.Repositories.Base;
using System.Threading.Tasks;
using MT.E_Sourcing.UI.Clients;

namespace MT.E_Sourcing.UI.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IUserRepository _userRepository;

        private readonly ProductClient _productClient;

        public AuctionController(IUserRepository userRepository, ProductClient productClient)
        {
            _userRepository = userRepository;
            _productClient = productClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            //TODO:Product GETALL

            var productList = await _productClient.GetProducts();

            if(productList.IsSuccess)
            {
                ViewBag.ProductList = productList.Data;
            }
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
