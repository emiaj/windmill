using System.Collections.Generic;

namespace Windmill.Core
{
    public interface IPermissionSource
    {
        IEnumerable<PermissionDescriptor> Permissions();  
    }
}