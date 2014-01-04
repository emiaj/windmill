using System.Collections.Generic;

namespace Windmill.Core
{
    public interface IPermissionProvider
    {
        void Insert(Permission permission);
        void Save(Permission permission);
        IEnumerable<Permission> List(IEnumerable<string> ids);
        Permission FindOneById(string id);
    }
}