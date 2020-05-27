using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageSystem.Models
{
    public class Language : ILanguage
    {
        public Language(string key)
        {
            KeyCode = key;
        }
        public string KeyCode { get; set; }
    }
}
