using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yandex360.Api.Models
{
    public class YandexUserName
    {
        public YandexUserName(string firstName, string midlleName, string lastName)
        {
            FirstName = firstName;
            MidlleName = midlleName;
            LastName = lastName;
        }

        [JsonProperty("first")]
        public string FirstName { get; set; }

        [JsonProperty("middle")]
        public string MidlleName { get; set; }

        [JsonProperty("last")]
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {MidlleName} {LastName}";
        }

        public static bool TryParse(string json, out YandexUserName name)
        {
            try
            {
                name = JsonConvert.DeserializeObject<YandexUserName>(json);
                return true;
            }
            catch
            {
                name = null;
                return false;
            }            
        }
    }
}
