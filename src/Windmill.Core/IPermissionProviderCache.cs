using System.Collections.Generic;

namespace Windmill.Core
{
    public interface IPermissionProviderCache
    {
        Permission Get(string id);
        IEnumerable<Permission> GetList(string id);
        IEnumerable<Permission> GetChildren(string id);
        void Update(Permission permission);
        IEnumerable<Permission> GetTopLevelPermissions();
    }
}