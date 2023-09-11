using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yandex360.Api.Models
{
    public class YandexUserContact
    {
        public YandexUserContact(string type, string value)
        {
            Type = type;
            Value = value;
        }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        public override string ToString()
        {
            return Type + ": " + Value;
        }
    }
}
