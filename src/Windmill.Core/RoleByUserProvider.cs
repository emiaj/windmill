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

        public string[] Get(string username)
        {
            return _roleProvider.GetRolesForUser(username);
        }
    }
}