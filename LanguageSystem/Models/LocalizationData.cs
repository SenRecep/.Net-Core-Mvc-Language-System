using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageSystem.Models
{
    public class LocalizationData
    {
        public LocalizationData()
        {

        }
        public LocalizationData(string key,string value)
        {
            this.Key = key;
            this.Value = value;
        }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
