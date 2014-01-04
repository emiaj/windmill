using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuCore.Util;

namespace Windmill.Core
{
    public class PermissionProviderCache : IPermissionProviderCache
    {
        private readonly IPermissionRegistryCache _cache;
        private readonly IPermissionProvider _provider;
        private readonly Cache<string, Permission> _getCache;
        private readonly Cache<string, IEnumerable<Permission>> _getListCache;
        private readonly Cache<string, IEnumerable<Permission>> _getChildrenCache;
        public PermissionProviderCache(IPermissionRegistryCache cache, IPermissionProvider provider)
        {
            _cache = cache;
            _provider = provider;
            _getCache = new Cache<string, Permission>(get);
            _getListCache = new Cache<string, IEnumerable<Permission>>(getList);
            _getChildrenCache = new Cache<string, IEnumerable<Permission>>(getChildren);
        }

        public Permission Get(string id)
        {
            return _getCache[id];
        }

        private Permission get(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (id.IsEmpty()) throw new ArgumentException("id cannot be empty", "id");
            return _provider.FindOneById(id) ?? insert(id);
        }

        public IEnumerable<Permission> GetList(string id)
        {
            return _getListCache[id];
        }

        private IEnumerable<Permission> getList(string id)
        {
            var parts = id.Split('/');
            var keys = parts.Select((t, i) => parts.Take(i + 1).Join("/")).ToArray();
            var permissions = _provider.List(keys).ToDictionary(x => x.Id);
            return keys.OrderBy(key => key)
                       .Select(key => permissions.ContainsKey(key) ? permissions[key] : insert(key))
                       .ToList();
        }

        public IEnumerable<Permission> GetChildren(string id)
        {
            return _getChildrenCache[id];
        }

        private IEnumerable<Permission> getChildren(string id)
        {
            return _cache.ChildrenOf(id).Select(Get);
        }

        public void Update(Permission permission)
        {
            _provider.Save(permission);
            _getCache.Remove(permission.Id);
            _getListCache.Remove(permission.Id);
            _getChildrenCache.Remove(permission.Id);
        }

        public IEnumerable<Permission> GetTopLevelPermissions()
        {
            return _cache.TopLevelPermissions().Select(Get);
        }

        private Permission insert(string id)
        {
            var parts = id.Split('/');
            Permission parent = null;

            if (parts.Length > 1)
            {
                parent = Get(parts.Take(parts.Length - 1).Join("/"));
            }

            var permission = parent == null ?
                new Permission(parts.Last(), null) :
                new Permission(parts.Last(), parent.Id);

            _cache.Get(id).Update(permission);

            _provider.Insert(permission);

            return permission;
        }
    }
}