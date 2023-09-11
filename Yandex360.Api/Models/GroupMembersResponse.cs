using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex360.Api.Models
{
    public sealed class GroupMembersResponse : ApiResponse
    {
        public List<YandexGroupMember> Data
        {
            get
            {
                var json = JObject.Parse(Json)["members"].ToString();
                var data = new List<YandexGroupMember>();

                if (TryParse<List<YandexGroupMember>>(json, out data))
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
