using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Yandex360.Api.Models;

namespace Yandex360.Api.Core
{
    public class Users : ConnectorBase
    {
        public Users(ApiOptions options) : base(options)
        {
        }

        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<UsersResponse> GetUsersAsync(int page = 1, int size = 20)
        {
            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/users?fields=name,position,department_id,nickname,contacts,gender&page={page}&per_page={size}",
                Method = HTTPMethod.GET
            });
            
            return response.Cast<UsersResponse>();
        }

        /// <summary>
        /// Получить сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<YandexUser> GetUserAsync(long id)
        {
            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/users/{id}/?fields=name,gender,position,contacts",
                Method = HTTPMethod.GET
            });

            var user = JsonConvert.DeserializeObject<YandexUser>(response.Json);

            return user;
        }

        /// <summary>
        /// Создать нового сотрудника
        /// </summary>
        /// <param name="json"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ApiResponse> CreateUserAsync(string json, string password = null)
        {
            var jobj = JObject.Parse(json);
            jobj.Add("password", password);
            jobj.Remove("id");

            json = jobj.ToString();

            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/users",
                Method = HTTPMethod.POST,
                Json = json
            });

            return response;
        }

        /// <summary>
        /// Обновить данные сотрудника
        /// </summary>
        /// <param name="user">сотрудник</param>
        public async Task<ApiResponse> UpdateUserAsync(YandexUser user)
        {
            var json = user.ToJson();

            // Нужно подготовить JSON потому что API не принимает избыточные данные.
            var jobj = JObject.Parse(json);
            jobj.Remove("id");
            jobj.Remove("nickname");

            if (user.DepartmentId == null)
            {
                jobj.Remove("department_id");
            }

            jobj.Remove("contacts");

            json = jobj.ToString();

            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/users/{user.Id}/",
                Method = HTTPMethod.PATH,
                Json = json
            });

            return response;
        }

        /// <summary>
        /// Сменить у сотрудника пароль
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<ApiResponse> ChangePasswordAsync(long userId, string newPassword)
        {
            var obj = new
            {
                password = newPassword,
            };

            var json = JsonConvert.SerializeObject(obj);

            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/users/{userId}/",
                Method = HTTPMethod.PATH,
                Json = json
            });

            return response;
        }

        /// <summary>
        /// Уволить сотрудника
        /// </summary>
        /// <param name="user">пользователь</param>
        public async Task<ApiResponse> DismissUserAsync(YandexUser user)
        {
            JObject jobj = new JObject
            {
                { "is_dismissed", JToken.FromObject(true) }
            };
            var json = jobj.ToString();

            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/users/{user.Id}/",
                Method = HTTPMethod.PATH,
                Json = json
            });

            return response;
        }

        /// <summary>
        /// Очистить сессию пользователя на всех устройствах
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApiResponse> LogoutUserAsync(long userId)
        {
            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/security/v1/org/{ApiOptions.OrganizationId}/domain_sessions/users/{userId}/logout",
                Method = HTTPMethod.PUT,
            });

            return response;
        }
    }
}
