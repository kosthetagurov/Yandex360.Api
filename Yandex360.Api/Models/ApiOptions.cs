using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Yandex360.Api.Models
{
    public sealed class ApiOptions
    {
        /// <summary>
        /// Oauth Token
        /// </summary>
        public string Token { get; private set; }
        /// <summary>
        /// Идентификатор организации
        /// </summary>
        public string OrganizationId { get; private set; }

        public Action<ApiResponse, string> Callback { get; private set; }

        /// <summary>
        /// Данные для доступа к api
        /// </summary>
        /// <param name="token"><see cref="ApiOptions.Token"/></param>
        /// <param name="organizationId"><see cref="ApiOptions.OrganizationId"/></param>
        /// <param name="callback">Делегат для обработки результатов запросов. Принимает <see cref="ApiResponse"/> и string Url запроса</param>
        public ApiOptions(string token, string organizationId, Action<ApiResponse, string> callback = null)
        {
            Callback = callback;
            Token = token;
            OrganizationId = organizationId;
        }
    }
}
