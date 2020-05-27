using LanguageSystem.Models;
using LanguageSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LanguageSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILanguageCookieService languageCookieService;

        public HomeController(ILanguageCookieService languageCookieService)
        {
            this.languageCookieService = languageCookieService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public JsonResult ChangeLanguage(string key)
        {
            try
            {
                Language language = new Language(key);
                languageCookieService.Set("Language", language, 60 * 60 * 7);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
