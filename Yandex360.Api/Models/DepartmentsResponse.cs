using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex360.Api.Models
{
    public sealed class DepartmentsResponse : ApiResponse
    {
        public List<YandexDepartment> Data
        {
            get
            {
                var json = JObject.Parse(Json)["departments"].ToString();
                var data = new List<YandexDepartment>();

                if (TryParse<List<YandexDepartment>>(json, out data))
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
