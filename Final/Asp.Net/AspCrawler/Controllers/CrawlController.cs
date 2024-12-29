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
        public IActionResult RunProgram(DateTime startTime, DateTime endTime)
        {
            string filePath = @"D:\Project\AspCrawler\AspCrawler\PythonExe\Crawler.exe";

            // بررسی اینکه تایم شروع قبل از پایان باشد
            if (startTime >= endTime)
            {
                TempData["Message"] = "Start time must be earlier than end time.";
                return RedirectToAction("Index", "Products");
            }

            try
            {
                while (startTime <= endTime)
                {
                    // اجرای فایل اجرایی
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    };
                    Process.Start(startInfo);

                    // صبر کردن برای دو دقیقه
                    Thread.Sleep(TimeSpan.FromMinutes(2));

                    // افزایش زمان شروع برای اجرای بعدی
                    startTime = startTime.AddMinutes(2);
                }

                TempData["Message"] = "Program executed successfully within the specified time range.";
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Error: {ex.Message}";
            }

            // هدایت به کنترلر Products و اکشن Index
            return RedirectToAction("Index", "Products");
        }
    }


}
