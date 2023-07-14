using PhoneApp.Domain;
using PhoneApp.Domain.DTO;
using PhoneApp.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneApp
{
    class Program
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Logger.Configure();
            logger.Info("Application started");
            try
            {
                Loader.LoadPlugins();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Trace(ex.StackTrace);
            }

            List<EmployeesDTO> dto = new List<EmployeesDTO>();

            foreach (var plugin in Loader.Plugins.Where(x => !x.GetType().Namespace.Contains("ViewerPlugin")))
            {
                dto.AddRange(plugin.Run(dto).Cast<EmployeesDTO>().ToList());
            }
            var view = Loader.Plugins.FirstOrDefault(x => x.GetType().Namespace.Contains("ViewerPlugin"));
            view.Run(dto);

            Console.WriteLine("Press any key to close application...");
            Console.ReadKey();
        }
    }
}
