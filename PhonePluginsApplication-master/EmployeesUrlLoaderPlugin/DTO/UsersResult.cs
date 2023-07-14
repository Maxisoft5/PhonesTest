using System.Collections.Generic;

namespace EmployeesUrlLoaderPlugin.DTO
{
    public class UsersResult
    {
        public IEnumerable<JSONEmployeesDTO> Users { get; set; }
    }
}
