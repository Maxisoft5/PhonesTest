using PhoneApp.Domain.DTO;
using System.Collections.Generic;
namespace PhoneApp.Domain.Interfaces
{
    public interface IPluggable
    {
        IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args);
    }
}
