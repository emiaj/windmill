using System.Collections.Generic;

namespace Windmill.Core
{
    public interface IPermissionRegistryCache
    {
        void Fill(PermissionDescriptor descriptor);
        PermissionDescriptor Get(string id);
        IEnumerable<string> TopLevelPermissions();
        IEnumerable<string> ChildrenOf(string id);
    }
}