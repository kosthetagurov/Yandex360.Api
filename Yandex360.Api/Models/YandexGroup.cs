using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yandex360.Api.Models
{
    public class YandexGroup
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("type")]
        public string GroupType { get; set; }

        [JsonProperty("author")]
        public YandexUser Author { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("members_count")]
        public string MembersCount { get; set; }        
    }
}
