using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex360.Api.Models
{
    public sealed class YandexGroupsResponse : ApiResponse
    {
        public List<YandexGroup> Data
        {
            get
            {
                var json = JObject.Parse(Json)["groups"].ToString();
                var data = new List<YandexGroup>();

                if (TryParse<List<YandexGroup>>(json, out data))
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
