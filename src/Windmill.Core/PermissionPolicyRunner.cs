using System.Collections.Generic;
using System.Linq;
using Bottles;
using Bottles.Diagnostics;

namespace Windmill.Core
{
    public class PermissionPolicyRunner : IActivator
    {
        private readonly IEnumerable<IPermissionSource> _sources;
        private readonly IPermissionRegistryCache _cache;

        public PermissionPolicyRunner(IEnumerable<IPermissionSource> sources, IPermissionRegistryCache cache)
        {
            _sources = sources;
            _cache = cache;
        }

        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            _sources.SelectMany(x => x.Permissions()).Each(_cache.Fill);
        }
    }
}