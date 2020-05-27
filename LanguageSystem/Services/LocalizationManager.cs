using LanguageSystem.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LanguageSystem.Services
{
    public class LocalizationManager : ILocalizationService
    {
        private ICollection<LocalizationData> localizationDatas;
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly ILanguageCookieService languageCookieService;
        private readonly string directoryPath;

        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Language { get; set; }
        public LocalizationManager(IActionContextAccessor actionContextAccessor, ILanguageCookieService languageCookieService)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.languageCookieService = languageCookieService;
            var rd = actionContextAccessor.ActionContext.RouteData;
            Controller = rd.Values["controller"].ToString();
            Action = rd.Values["action"].ToString();
            Area = rd.Values["area"]?.ToString();
            directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
        }
        private void LoadLanguage()
        {
            if (Language==null)
            {
                Language language = languageCookieService.Get("Language");
                if (language?.KeyCode == null)
                {
                    language = new Language("tr");
                    languageCookieService.Set("Language", language, 60 * 24 * 7);
                }
                Language = language.KeyCode; 
            }
        }

        private void LoadWords(string file)
        {
            if (File.Exists(file))
            {
                string json = File.ReadAllText(file);
                localizationDatas = JsonConvert.DeserializeObject<List<LocalizationData>>(json);
            }
        }
        public string get(string key)
        {
            return localizationDatas?.FirstOrDefault(x => x.Key.Equals(key))?.Value ?? "NO CONTENT";
        }
        public void UseLayoutWords(ViewContext context)
        {
            LoadLanguage();
            var layoutName = ((RazorView)context.View).ViewStartPages.FirstOrDefault().Layout;
            string file;
            if (string.IsNullOrWhiteSpace(Area))
                file = Path.Combine(directoryPath, $"{layoutName}.{Language}.json");
            else
                file = Path.Combine(directoryPath, $"{Area}.{layoutName}.{Language}.json");
            LoadWords(file);
        }

        public void UseActionWords()
        {
            LoadLanguage();
            string file;
            if (string.IsNullOrWhiteSpace(Area))
                file = Path.Combine(directoryPath, $"{Controller}.{Action}.{Language}.json");
            else
                file = Path.Combine(directoryPath, $"{Area}.{Controller}.{Action}.{Language}.json");
            LoadWords(file);
        }
    }
}
