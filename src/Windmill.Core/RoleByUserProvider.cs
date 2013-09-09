using System.Web.Security;

namespace Windmill.Core
{
    public class RoleByUserProvider : IRoleByUserProvider
    {
        private readonly RoleProvider _roleProvider;

        public RoleByUserProvider(RoleProvider roleProvider)
        {
            _roleProvider = roleProvider;
        }

        public string[] GetRolesForUser(string username)
        {
            return _roleProvider.GetRolesForUser(username);
        }
    }
}