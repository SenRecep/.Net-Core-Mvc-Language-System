using Microsoft.AspNetCore.Mvc.Rendering;

namespace LanguageSystem.Services
{
    public interface ILocalizationService
    {
        string Controller { get; set; }
        string Language { get; set; }
        string get(string key);
        void UseLayoutWords(ViewContext context);
        void UseActionWords();
    }
}
