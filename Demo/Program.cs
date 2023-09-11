using Yandex360.Api.Core;
using Yandex360.Api.Models;

namespace Demo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, Yandex360.Api!");

            string token = ""; // Put your oauth token here
            string organizationId = ""; // Put your organization id here

            var options = new ApiOptions(token, organizationId, HandleCallback);

            var usersManager = new Users(options);
            var users = await usersManager.GetUsersAsync();

            var departmentsManager = new Departments(options);
            var departments = await departmentsManager.GetDepartmentsAsync();

            var groupsManager = new Groups(options);
            var groups = await groupsManager.GetGroupsAsync();

            Console.ReadKey();
        }

        static void HandleCallback(ApiResponse apiResponse, string url)
        {
            Console.WriteLine(apiResponse.ToString());
        }
    }
}