﻿using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeesViewerPlugin
{

    [Author(Name = "Ivan Petrov")]
    public class Plugin : IPluggable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            logger.Info("Starting Viewer");
            logger.Info("Type q or quit to exit");
            logger.Info("Available commands: list, add, del");

            var employeesList = args.Cast<EmployeesDTO>().ToList();

            string command = "";

            while (!command.ToLower().Contains("quit"))
            {
                Console.Write("> ");
                command = Console.ReadLine();

                switch (command)
                {
                    case "list":
                        int index = 0;
                        foreach (var employee in employeesList)
                        {
                            Console.WriteLine($"{index} Name: {employee.Name} | Phone: {employee.Phone}");
                            ++index;
                        }
                        break;
                    case "add":
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Phone: ");
                        string phone = Console.ReadLine();
                        employeesList.Add(new EmployeesDTO
                        {
                            Name = name,
                            Phone = phone
                        });
                        Console.WriteLine($"{name} added to employees");
                        break;
                    case "del":
                        Console.Write("Index of employee to delete: ");
                        int indexToDelete;
                        if (!Int32.TryParse(Console.ReadLine(), out indexToDelete))
                        {
                            logger.Error("Not an index or not an int value!");
                        }
                        else
                        {
                            if (indexToDelete > 0 && indexToDelete < employeesList.Count())
                            {
                                employeesList.RemoveAt(indexToDelete);
                            }
                        }
                        break;
                }

                Console.WriteLine("");
            }

            return employeesList.Cast<DataTransferObject>();
        }
    }
}
