using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

namespace AspCrawler.Controllers
{
    public class CrawlController : Controller
    {
        private readonly ILogger<CrawlController> _logger;

        public CrawlController(ILogger<CrawlController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RunProgram()
        {
            string filePath = @"D:\Project\AspCrawler\AspCrawler\PythonExe\s01.exe";
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true // Allows running .exe with default settings
                };
                Process.Start(startInfo);
                TempData["Message"] = "Program executed successfully.";
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Error: {ex.Message}";
            }

            // Redirect to the Index action in the Products controller
            return RedirectToAction("Index", "Products");
        }
    }


}
