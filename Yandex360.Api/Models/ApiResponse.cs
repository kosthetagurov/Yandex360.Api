using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Yandex360.Api.Models
{
    /// <summary>
    /// Ответ Yandex.360 Api
    /// </summary>
    public class ApiResponse : IJsonParseble
    {
        public string Json { get; internal set; }
        public HttpStatusCode StatusCode { get; internal set; }

        public int Total => TryGetTotal();

        public bool TryParse<T>(string json, out T dest)
        {
            try
            {
                dest = JsonConvert.DeserializeObject<T>(json);
                return true;
            }
            catch
            {
                dest = default(T);
                return false;
            }
        }

        private int TryGetTotal()
        {
            if (string.IsNullOrEmpty(Json))
            {
                return 0;
            }

            try
            {
                var jobj = JObject.Parse(Json);
                var total = jobj["total"];

                if (total == null)
                {
                    return 0;
                }

                if (int.TryParse(total.ToString(), out int _total))
                {
                    return _total;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public T Cast<T>() where T : ApiResponse
        {
            var serializedParent = JsonConvert.SerializeObject(this);
            T castedObject = JsonConvert.DeserializeObject<T>(serializedParent);
            return castedObject;
        }

        public override string ToString()
        {
            return $"Status: {StatusCode}. Response: {Json}";
        }
    }
}
