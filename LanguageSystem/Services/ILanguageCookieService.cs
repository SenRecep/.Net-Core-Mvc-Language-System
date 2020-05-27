using LanguageSystem.Models;

namespace LanguageSystem.Services
{
    public interface ILanguageCookieService
    {
        void Set(string key, object value, int? expireTime);
        Language Get(string key);
        void Remove(string key);
    }
}
