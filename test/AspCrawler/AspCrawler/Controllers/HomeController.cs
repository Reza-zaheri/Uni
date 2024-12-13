using AspCrawler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var products = await _context.pr_digis
                .OrderBy(p => p.p_id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
            .ToListAsync();

            var totalProducts = await _context.pr_digis.CountAsync();

            var viewModel = new ProductListViewModel
            {
                Products = products,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize)
            };

            return View(viewModel);
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
