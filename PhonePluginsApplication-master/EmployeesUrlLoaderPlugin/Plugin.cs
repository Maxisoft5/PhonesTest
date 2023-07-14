using EmployeesUrlLoaderPlugin.DTO;
using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Linq;


namespace EmployeesUrlLoaderPlugin
{
    [Author(Name = "Maskym Gryniuk")]
    public class Plugin : IPluggable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            var options = new RestClientOptions("https://dummyjson.com");

            var client = new RestClient(options);
            var request = new RestRequest("users");
            var users = client.GetAsync<UsersResult>(request).ConfigureAwait(false).GetAwaiter().GetResult();
            var usersDTOs = new List<EmployeesDTO>();
            foreach (var user in users.Users)
            {
                var emp = new EmployeesDTO()
                {
                    Name = user.FirstName
                };
                emp.AddPhone(user.Phone);
                usersDTOs.Add(emp);
            }
            return usersDTOs;
        }
    }
}
