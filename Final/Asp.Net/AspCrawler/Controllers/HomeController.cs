using AspCrawler.Models;
using AspCrawler.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AspCrawler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories
                .Select(category => new ProductCategoryViewModel
                {
                    CategoryTitle = category.Title,
                    Products = _context.Products
                                .Where(p => p.CatId == category.Id)
                                .ToList()
                }).ToList();

            return View(categories);
        }
        public IActionResult Details(int id)
        {
            // پیدا کردن محصول بر اساس ID
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); // اگر محصول وجود نداشت
            }

            // دریافت دسته‌بندی مرتبط با محصول
            var category = _context.Categories.FirstOrDefault(c => c.Id == product.CatId);

            // واکشی تغییرات محصول از جدول Enrolls
            var enrollChanges = _context.Enrolls
                .Where(e => e.IdP == id)
                .OrderByDescending(e => e.Time)
                .ToList();

            // ارسال داده به ویو
            var model = new ProductDetailsViewModel
            {
                Product = product,
                CategoryTitle = category?.Title ?? "بدون دسته‌بندی",
                EnrollChanges = enrollChanges
            };

            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
