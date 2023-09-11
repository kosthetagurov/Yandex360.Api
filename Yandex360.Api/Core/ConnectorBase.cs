using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Yandex360.Api.Models;
using System.Diagnostics;

namespace Yandex360.Api.Core
{
    public enum HTTPMethod
    {
        GET,
        POST,
        PATH,
        DELETE,
        PUT
    }

    public abstract class ConnectorBase
    {
        protected const string Host = "https://api360.yandex.net";
        protected const string ContentType = "application/json; charset=utf-8";
        protected const string Accept = "application/json";

        protected readonly ApiOptions ApiOptions;
        protected readonly string Authorization;

        public ConnectorBase(ApiOptions options)
        {
            ApiOptions = options;
            Authorization = $"OAuth {options.Token}";
        }

        public async Task<ApiResponse> SendRequsetAsync(RequestOptions requestOptions)
        {
            if (requestOptions == null)
            {
                throw new ArgumentNullException(nameof(RequestOptions), $"{nameof(RequestOptions)} must be NOT NULL");
            }

            using var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(requestOptions.Url);
            httpClient.DefaultRequestHeaders.Accept.Clear();

            httpClient.DefaultRequestHeaders.Add(nameof(Authorization), Authorization);
            if (requestOptions.Method == HTTPMethod.POST || requestOptions.Method == HTTPMethod.PATH || requestOptions.Method == HTTPMethod.PUT)
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Accept));
            }

            if (requestOptions.Method == HTTPMethod.GET)
            {
                httpClient.DefaultRequestHeaders.Add(nameof(Accept), Accept);
            }

            var response = new HttpResponseMessage();

            StringContent? content = null;
            if (!string.IsNullOrEmpty(requestOptions.Json))
            {
                content = new StringContent(requestOptions.Json, Encoding.UTF8, "application/json");
            }

            switch (requestOptions.Method)
            {
                case HTTPMethod.POST:
                    response = await httpClient.PostAsync(requestOptions.Url, content);
                    break;
                case HTTPMethod.GET:
                    response = await httpClient.GetAsync(requestOptions.Url);
                    break;
                case HTTPMethod.PATH:
                    response = await httpClient.PatchAsync(requestOptions.Url, content);
                    break;
                case HTTPMethod.DELETE:
                    response = await httpClient.DeleteAsync(requestOptions.Url);
                    break;
                case HTTPMethod.PUT:
                    response = await httpClient.PutAsync(requestOptions.Url, null);
                    break;
            }

            string result = "";
            try
            {
                result = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message + " " + exc.StackTrace);
            }

            var yaResponse = new ApiResponse { Json = result, StatusCode = response.StatusCode };
            if (ApiOptions.Callback != null)
            {
                ApiOptions.Callback.Invoke(yaResponse, requestOptions.Url);
            }

            return yaResponse;
        }
    }
}
