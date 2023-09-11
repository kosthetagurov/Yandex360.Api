using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Yandex360.Api.Models
{
    public class YandexUser
    {
        [Obsolete(message: "This method is obsolete", error: true)]
        public static YandexUser InMemoryCreate(string nickName, int departmentId, YandexUserName name, params YandexUserContact[] contacts)
        {
            if (string.IsNullOrEmpty(nickName))
            {
                throw new ArgumentNullException(nameof(nickName));
            }

            return new YandexUser()
            {
                DepartmentId = departmentId,
                NickName = nickName,
                Name = name,
                Contacts = contacts?.ToList()
            };
        }

        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("department_id")]
        public int? DepartmentId { get; set; }

        [JsonProperty("name")]
        public YandexUserName Name { get; set; }

        [JsonProperty("nickname")]
        public string NickName { get; set; }

        [JsonProperty("contacts")]
        public List<YandexUserContact> Contacts { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonIgnore]
        public string FullName => $"{Name.FirstName} {Name.MidlleName} {Name.LastName}";

        public string ToJson()
        {
            var json = JsonConvert.SerializeObject(this);
            return json;
        }
    }
}
