using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex360.Api.Models
{
    public sealed class UsersResponse : ApiResponse
    {
        public List<YandexUser> Data
        {
            get
            {
                var json = JObject.Parse(Json)["users"].ToString();
                var data = new List<YandexUser>();

                if (TryParse<List<YandexUser>>(json, out data))
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

