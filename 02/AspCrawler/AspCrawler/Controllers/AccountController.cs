using AspCrawler.Models;
using AspCrawler.Models.Dto;
using AspCrawler.Models.Dto.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspCrawler.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
       
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterDto register)
        {
            TempData["Message"] = "";
            if (ModelState.IsValid == false)
            {
                return View(register);
            }
            User newUser = new User()
            {
                Fname = register.FirstName,
                Lname = register.LastName,
                Email = register.Email,
                UserName = register.Email,
            };
            var result = _userManager.CreateAsync(newUser, register.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            string mess = "";
            foreach (var item in result.Errors.ToList())
            {
                mess += item.Description + Environment.NewLine;
            }
            TempData["Message"] = mess;
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginDto { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            TempData["Message"] = "";
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                TempData["Message"] = "نام کاربری یا رمز عبور اشتباه است";
                return View(login);
            }

            await _signInManager.SignOutAsync();

            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
            if (result.Succeeded)
            {
                return Redirect(login.ReturnUrl);
            }

            TempData["Message"] = "نام کاربری یا رمز عبور اشتباه است";
            return View(login);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
