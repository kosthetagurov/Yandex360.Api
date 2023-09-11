using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yandex360.Api.Models
{
    public class YandexDepartment : YandexDepartmentBase
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("removed")]
        public bool IsRemoved { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("members_count")]
        public int MembersCount { get; set; }

        [JsonProperty("head")]
        public long Head { get; set; }

        [JsonProperty("parent")]
        public YandexDepartmentBase Parent { get; set; }

    }
}
