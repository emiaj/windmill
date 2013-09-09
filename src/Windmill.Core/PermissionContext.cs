using System.Collections.Generic;
using System.Linq;
using FubuCore.Util;
using FubuMVC.Core.Security;

namespace Windmill.Core
{
    public class PermissionContext : IPermissionContext
    {
        private readonly IPermissionProviderCache _permissionProviderCache;
        private readonly ISecurityContext _securityContext;
        private readonly IRoleByUserProvider _roleByUserProvider;
        private readonly Cache<string, AuthorizationRight> _cache = new Cache<string, AuthorizationRight>();
        public PermissionContext(IPermissionProviderCache permissionProviderCache, ISecurityContext securityContext,
                                 IRoleByUserProvider roleByUserProvider)
        {
            _permissionProviderCache = permissionProviderCache;
            _securityContext = securityContext;
            _roleByUserProvider = roleByUserProvider;
            _cache.OnMissing = rightsToCache;
        }

        public bool Check(string permission)
        {
            return Check(new[] { permission });
        }

        public bool Check(IEnumerable<string> permissions)
        {
            if (permissions != null && permissions.Any())
            {
                var authorizationRights = permissions.Select(RightsTo);
                var authorizationRight = AuthorizationRight.Combine(authorizationRights);
                return authorizationRight == AuthorizationRight.Allow;
            }
            return true;
        }

        public AuthorizationRight RightsTo(string permission)
        {
            return _cache[permission];
        }

        private AuthorizationRight rightsToCache(string permission)
        {
            var username = _securityContext.CurrentIdentity.Name;
            var roles = _roleByUserProvider.Get(username);
            var rights = rightsTo(permission, username, roles);
            if (rights.Any(x => x == AuthorizationRight.Deny))
            {
                return AuthorizationRight.Deny;
            }
            return AuthorizationRight.Allow;
        }


        private IEnumerable<AuthorizationRight> rightsTo(string id, string user, string[] roles)
        {
            return _permissionProviderCache.GetList(id).Select(permission => rightsTo(permission, user, roles));
        }

        private static AuthorizationRight rightsTo(Permission permission, string user, string[] roles)
        {
            if (permission.AllowedUsers.Contains(user))
            {
                 return AuthorizationRight.Allow;
            }
            if (permission.ForbiddenUsers.Contains(user))
            {
                 return AuthorizationRight.Deny;
            }
            if (roles.Any(role => permission.AllowedGroups.Any(allowed => role == allowed)))
            {
                 return AuthorizationRight.Allow;
            }
            if (roles.Any(role => permission.ForbiddenGroups.Any(forbidden => role == forbidden)))
            {
                 return AuthorizationRight.Deny;
            }
            return AuthorizationRight.Allow;
        } 

    }
}
