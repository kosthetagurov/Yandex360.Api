using Yandex360.Api.JsonConverters;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yandex360.Api.Models
{
    public class YandexGroupMemberObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonConverter(typeof(NameConverter))]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
