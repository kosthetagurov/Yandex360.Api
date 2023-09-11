using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yandex360.Api.Models
{
    public interface IJsonParseble
    {
        bool TryParse<T>(string json, out T dest);
    }
}
