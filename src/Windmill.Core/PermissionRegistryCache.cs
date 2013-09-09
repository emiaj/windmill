using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuCore.Util;

namespace Windmill.Core
{
    public class PermissionRegistryCache : IPermissionRegistryCache
    {
        private readonly Cache<string, PermissionDescriptor> _cache = new Cache<string, PermissionDescriptor>(x => new PermissionDescriptor(x));

        public void Fill(PermissionDescriptor descriptor)
        {
            _cache.Fill(descriptor.Id, descriptor);
        }

        public PermissionDescriptor Get(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (id.IsEmpty()) throw new ArgumentException("id cannot be empty", id);
            return _cache[id];
        }

        public IEnumerable<string> TopLevelPermissions()
        {
            return _cache.GetAllKeys().Where(x => x.Split('/').Length == 1).OrderBy(x => x);
        }

        public IEnumerable<string> ChildrenOf(string id)
        {
            var parts = id.Split('/');
            return _cache.GetAllKeys().Where(x =>
            {
                var idParts = x.Split('/');
                if (idParts.Length != parts.Length + 1)
                {
                    return false;
                }
                if(!x.StartsWith(id + "/"))
                {
                    return false;
                }
                return true;
            });
        }
    }
}