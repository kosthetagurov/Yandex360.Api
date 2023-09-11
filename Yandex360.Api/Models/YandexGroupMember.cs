using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yandex360.Api.Models
{
    public class YandexGroupMember
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("object")]
        public YandexGroupMemberObject Object { get; set; }
    }
}
