using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Yandex360.Api.Core;

namespace Yandex360.Api.Models
{
    public class RequestOptions
    {
        public string Url { get; set; }
        public string Json { get; set; }
        public HTTPMethod Method { get; set; }
    }
}
