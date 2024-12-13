using AspCrawler.Models;
using AspCrawler.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspCrawler.Controllers
{
    
    public class UserManageController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly Context db;
        private readonly ILogger<UserManageController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UserManageController(ILogger<UserManageController> logger, Context context, IWebHostEnvironment WebHostEnvironment, UserManager<User> userManager)
        {
            db = context;
            _logger = logger;
            webHostEnvironment = WebHostEnvironment;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.Select(p => new UserListDto
            {
                Id = p.Id,
                Fname = p.Fname,
                Lname = p.Lname,
                Username = p.UserName,
                EmailConfirmed = p.EmailConfirmed,
                AccessFailedCount = p.AccessFailedCount
            }).ToList();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RegisterDto register)
        {
            if (!ModelState.IsValid)
                return View(register);

            var newUser = new User
            {
                Fname = register.FirstName,
                Lname = register.LastName,
                Email = register.Email,
                UserName = register.Email
            };

            var result = _userManager.CreateAsync(newUser, register.Password).Result;
            if (result.Succeeded)
                return RedirectToAction("Index", "UserManage");

            TempData["Message"] = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
            return View(register);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["Message"] = "کاربر پیدا نشد.";
                return RedirectToAction("Index");
            }
            var userEdit = new UserEditDto
            {
                Id = user.Id,
                FirstName = user.Fname,
                LastName = user.Lname,
                Email = user.Email,
                UserName = user.UserName
            };

            return View(userEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditDto userEdit)
        {
            var user = await _userManager.FindByIdAsync(userEdit.Id);
            if (user == null)
            {
                TempData["Message"] = "کاربر پیدا نشد.";
                return RedirectToAction("Index");
            }

            user.Fname = userEdit.FirstName;
            user.Lname = userEdit.LastName;
            user.Email = userEdit.Email;
            user.UserName = userEdit.UserName;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return RedirectToAction("Index", "UserManage");

            TempData["Message"] = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
            return View(userEdit);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["Message"] = "کاربر پیدا نشد.";
                return RedirectToAction("Index");
            }

            var userDelete = new UserDeleteDto
            {
                Email = user.Email,
                FullName = $"{user.Fname} {user.Lname}",
                Id = user.Id,
                UserName = user.UserName
            };

            return View(userDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UserDeleteDto userDelete)
        {
            var user = await _userManager.FindByIdAsync(userDelete.Id);
            if (user == null)
            {
                TempData["Message"] = "کاربر پیدا نشد.";
                return RedirectToAction("Index");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return RedirectToAction("Index", "UserManage");

            TempData["Message"] = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
            return View(userDelete);
        }
    }

}

