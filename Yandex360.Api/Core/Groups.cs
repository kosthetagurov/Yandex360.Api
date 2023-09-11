using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Yandex360.Api.Models;

namespace Yandex360.Api.Core
{
    public enum MemberType
    {
        User,
        Group,
        Department
    }

    public class Groups : ConnectorBase
    {
        public Groups(ApiOptions options) : base(options)
        {
        }

        /// <summary>
        /// Добавить участника в группу
        /// </summary>
        /// <param name="group"></param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        public async Task<ApiResponse> AddMemberAsync(string groupId, MemberType type, object id)
        {
            var options = new
            {
                type = type.ToString().ToLower(),
                id = id
            };

            var optionsJson = JsonConvert.SerializeObject(options);

            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/groups/{groupId}/members/",
                Method = HTTPMethod.POST,
                Json = optionsJson
            });

            return response;
        }

        /// <summary>
        /// Удалить участника из группы
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse> RemoveMemberAsync(string groupId, string type, object id)
        {
            var obj = new
            {
                operation_type = "remove",
                value = new
                {
                    type = type.ToLower(),
                    id = long.Parse(id.ToString())
                }
            };

            var data = new object[] { obj };

            var optionsJson = JsonConvert.SerializeObject(data);

            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/groups/{groupId}/members/bulk-update/",
                Method = HTTPMethod.POST,
                Json = optionsJson
            });

            return response;
        }

        /// <summary>
        /// Получить участников группы
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task<GroupMembersResponse> GetGroupMembersAsync(YandexGroup group)
        {
            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/groups/{group.Id}/?fields=members",
                Method = HTTPMethod.GET
            });

            return response.Cast<GroupMembersResponse>();
        }

        /// <summary>
        /// Получить список групп
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<YandexGroupsResponse> GetGroupsAsync(int page = 1, int size = 20)
        {
            var response = await SendRequsetAsync(new RequestOptions()
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/groups/?fields=id,name,email,label,created,type,author,description,members_count&page={page}&per_page={size}",
                Method = HTTPMethod.GET
            });

            return response.Cast<YandexGroupsResponse>();
        }

        /// <summary>
        /// Создать группу
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public async Task<ApiResponse> CreateGroupAsync(string json)
        {
            var jobj = JObject.Parse(json);
            var toRemove = new string[] { "members_count", "email", "id", "author", "created" };

            foreach (var key in toRemove)
            {
                jobj.Remove(key);
            }

            json = jobj.ToString();

            var response = await SendRequsetAsync(new RequestOptions
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/groups/",
                Method = HTTPMethod.POST,
                Json = json
            });

            return response;
        }

        /// <summary>
        /// Обновить информацию о группе
        /// </summary>
        /// <param name="json"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse> UpdateGroupAsync(string json, string id)
        {
            var jobj = JObject.Parse(json);
            var toRemove = new string[] { "members_count", "email", "id", "author", "created", "type" };

            foreach (var key in toRemove)
            {
                jobj.Remove(key);
            }

            json = jobj.ToString();

            var response = await SendRequsetAsync(new RequestOptions
            {
                Url = $"{Host}/directory/v1/org/{ApiOptions.OrganizationId}/groups/{id}/",
                Method = HTTPMethod.PATH,
                Json = json
            });

            return response;
        }
    }
}
