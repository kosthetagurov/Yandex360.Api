using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yandex360.Api.Models;

namespace Yandex360.Api.JsonConverters
{
    public class NameConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => true;

        /// <summary>
        /// Для конвертации имени пользователя при получении участников команды.
        /// </summary>
        public override string ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObj = JObject.Load(reader);
            var json = jObj.ToString();

            if (YandexUserName.TryParse(json, out YandexUserName name))
            {
                return name.ToString();
            }
            else
            {
                return json;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        { }
    }
}
