using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EmployeesLoaderPlugin
{

    [Author(Name = "Ivan Petrov")]
    public class Plugin : IPluggable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            logger.Info("Loading employees");

            var employeesList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmployeesDTO>>(Properties.Resources.EmployeesJson);

            logger.Info($"Loaded {employeesList.Count()} employees");
            return employeesList.Cast<DataTransferObject>();
        }
    }
}
