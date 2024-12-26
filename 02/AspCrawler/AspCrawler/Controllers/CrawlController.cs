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
        public async Task<IActionResult> ExecuteFile(string startTime, string endTime)
        {
            // تبدیل زمان شروع و پایان از رشته به TimeSpan
            if (TryParseTimeRanges(startTime, endTime, out TimeSpan startTimeSpan, out TimeSpan endTimeSpan))
            {
                TimeSpan currentTime = DateTime.Now.TimeOfDay;

                // بررسی اگر زمان شروع در آینده باشد
                if (currentTime < startTimeSpan)
                {
                    _logger.LogInformation("در حال انتظار برای زمان شروع: {StartTime}", startTimeSpan);
                    await TaskExtensions.DelayUntil(startTimeSpan);
                }

                while (currentTime <= endTimeSpan)
                {
                    _logger.LogInformation("در حال اجرای فرآیند در زمان: {CurrentTime}", currentTime);

                    // اجرای فایل EXE
                    ExecuteProcess();

                    // انتظار برای 2 دقیقه قبل از اجرای دوباره
                    await Task.Delay(TimeSpan.FromMinutes(2));
                    currentTime = DateTime.Now.TimeOfDay; // به روز رسانی زمان جاری
                }

                TempData["SuccessMessage"] = "اجرای برنامه با موفقیت انجام شد.";
                return RedirectToAction("Index");
            }

            return BadRequest("زمان شروع یا پایان معتبر نیست.");
        }

        private bool TryParseTimeRanges(string startTime, string endTime, out TimeSpan startTimeSpan, out TimeSpan endTimeSpan)
        {
            bool isStartTimeParsed = TimeSpan.TryParse(startTime, out startTimeSpan);
            bool isEndTimeParsed = TimeSpan.TryParse(endTime, out endTimeSpan);

            return isStartTimeParsed && isEndTimeParsed;
        }

        private void ExecuteProcess()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"D:\Project\AspCrawler\AspCrawler\PythonExe\MyProgram.exe", // مسیر صحیح فایل EXE را قرار دهید
                        Arguments = "", // در صورت نیاز آرگومان‌های لازم را اضافه کنید
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    _logger.LogError("فرآیند با خطا مواجه شد. کد خروج: {ExitCode}", process.ExitCode);
                }
                else
                {
                    _logger.LogInformation("فرآیند با موفقیت اجرا شد.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("یک خطا در هنگام اجرای فرآیند رخ داد: {ErrorMessage}", ex.Message);
            }
        }
    }

    public static class TaskExtensions
    {
        public static async Task DelayUntil(TimeSpan targetTime)
        {
            TimeSpan delay = targetTime - DateTime.Now.TimeOfDay;
            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay);
            }
        }
    }


}
