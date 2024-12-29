using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCrawler.Models.Dto;
using AspCrawler.Models.Dto.Roles;
using AspCrawler.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AspCrawler.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public RolesController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var rolse = _roleManager.Roles
                .Select(p=>
                 new RoleListDto
                 {
                      Id= p.Id,
                      Description=p.Descirption,
                      Name=p.Name
                 })
                .ToList();

            return View(rolse);
        }

        [HttpGet]
        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddNewRoleDto newRole)
        {
            Role role = new Role()
            {
                Descirption = newRole.Description,
                Name = newRole.Name,

            };
            var result= _roleManager.CreateAsync(role).Result;
            
            //_roleManager.UpdateAsync()
            //_roleManager.DeleteAsync()
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Roles");
            };
            ViewBag.Errors= result.Errors.ToList();
            return View(newRole);

        }
        public async Task<IActionResult> UserInRole(string Name)
        {
            // بررسی مقدار Name
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest("Role name cannot be null or empty.");
            }

            // دریافت کاربران در نقش
            var usersInRole = await _userManager.GetUsersInRoleAsync(Name);

            // اگر هیچ کاربری در نقش مشخص نباشد
            if (usersInRole == null || !usersInRole.Any())
            {
                return NotFound($"No users found in the role '{Name}'.");
            }

            // تبدیل کاربران به UserListDto
            var userList = usersInRole.Select(p => new UserListDto
            {
                Fname = p.Fname,
                Lname = p.Lname,
                Username = p.UserName,
                Id = p.Id,
            });

            return View(userList);
        }

    }
}
