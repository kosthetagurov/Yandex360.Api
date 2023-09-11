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
    public class Departments : ConnectorBase
    {
        public Departments(ApiOptions options) : base(options)
        {
        }

        /// <summary>
        /// Получить подразделения
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<DepartmentsResponse> GetDepartmentsAsync(int page = 1, int size = 20)
        {
            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/departments/?fields=name,email,external_id,removed,parent,label,created,description,members_count,head&page={page}&per_page={size}",
                Method = HTTPMethod.GET
            });

            return response.Cast<DepartmentsResponse>();
        }

        /// <summary>
        /// Удалить подразделение
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ApiResponse> DeleteDepartmentAsync(YandexDepartment department)
        {
            if (department.Id == 0)
            {
                throw new ArgumentException(nameof(YandexDepartment), new Exception($"Argument [YandexDepartment department] is invalid. Id is 0"));
            }

            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/departments/{department.Id}/",
                Method = HTTPMethod.DELETE,
            });

            return response;
        }

        /// <summary>
        /// Обновить данные подразделения
        /// </summary>
        /// <param name="department"></param>
        /// <param name="parentId"></param>
        /// <param name="headId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ApiResponse> UpdateDepartmentAsync(YandexDepartment department, int parentId, long headId)
        {
            if (department.Id == 0)
            {
                throw new ArgumentException(nameof(YandexDepartment), new Exception($"Argument [YandexDepartment department] is invalid. Id is 0"));
            }

            var postData = department.ToJson();

            // Готовлю json к отправке, делаю так что бы не писать отдельный функционал.
            // Проще очистить модель и отправить так.
            var jObj = JObject.Parse(postData);
            jObj.Remove("parent");
            var allKeys = jObj.ToObject<Dictionary<string, string>>().Keys;
            var needKeys = new string[] { "description", "head_id", "label", "name", "parent_id" };

            foreach (var key in allKeys)
            {
                if (!needKeys.Contains(key))
                {
                    jObj.Remove(key);
                }
            }

            jObj.Add("parent_id", parentId <= 0 ? null : parentId);
            jObj.Add("head_id", headId <= 0 ? null : headId);

            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/departments/{department.Id}/",
                Method = HTTPMethod.PATH,
                Json = jObj.ToString()
            });

            return response;
        }

        /// <summary>
        /// Создать подразделение
        /// </summary>
        /// <param name="department"></param>
        /// <param name="parentId"></param>
        /// <param name="headId"></param>
        /// <returns></returns>
        public async Task<ApiResponse> CreateDepartmentAsync(YandexDepartment department, int parentId, long headId)
        {
            var postData = department.ToJson();

            var jObj = JObject.Parse(postData);
            var allKeys = jObj.ToObject<Dictionary<string, string>>().Keys;
            var needKeys = new string[] { "description", "head_id", "label", "name", "parent_id" };

            foreach (var key in allKeys)
            {
                if (!needKeys.Contains(key))
                {
                    jObj.Remove(key);
                }
            }

            jObj.Add("parent_id", parentId <= 0 ? null : parentId);
            jObj.Add("head_id", headId <= 0 ? null : headId);

            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/departments/",
                Method = HTTPMethod.POST,
                Json = jObj.ToString()
            });

            return response;
        }
    }
}
