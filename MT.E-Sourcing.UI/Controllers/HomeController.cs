using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MT.E_Sourcing.UI.ViewModel;
using MT.E_Sourcing.WebApp.Core.Entities;
using System.Threading.Tasks;

namespace MT.E_Sourcing.UI.Controllers
{
    public class HomeController : Controller
    {

        public UserManager<AppUser> _userManager { get; } //readonly

        public SignInManager<AppUser> _signInManager { get; }

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
  
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel, string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

                    if(result.Succeeded)
                    {
                        HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email  or password are not valid");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email  or password are not valid");
                }
            }
            
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(AppUserViewModel signUpModel)
        {

            if(ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.FirstName = signUpModel.FirstName;
                user.Email = signUpModel.Email;
                user.LasttName = signUpModel.LastName; 
                user.PhoneNumber = signUpModel.PhoneNumber;
                user.UserName = signUpModel.UserName;
                if (signUpModel.UserSelectTypeId == 1) { user.IsBuyer = true; user.IsSeller = false; }
                else
                { user.IsBuyer = false; user.IsSeller = true; }

                var result = await _userManager.CreateAsync(user, signUpModel.Password);

                if (result.Succeeded)
                    return RedirectToAction("Login");
                else
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
           await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
