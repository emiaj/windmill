using System.Collections.Generic;
using System.Linq;

namespace Windmill.Core
{
    public class InMemoryPermissionProvider : IPermissionProvider
    {
        private readonly List<Permission> _permissions = new List<Permission>();


        public void Insert(Permission permission)
        {
            _permissions.Add(permission);
        }

        public void Save(Permission permission)
        {
            _permissions[_permissions.FindLastIndex(e => e.Id == permission.Id)] = permission;
        }

        public IEnumerable<Permission> List(string id)
        {
            return _permissions.Where(x => x.Id == id || x.Id.StartsWith(id));
        }

        public Permission FindOneById(string id)
        {
            return _permissions.FirstOrDefault(x => x.Id == id);
        }
    }
}