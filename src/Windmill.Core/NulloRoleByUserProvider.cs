namespace Windmill.Core
{
    public class NulloRoleByUserProvider : IRoleByUserProvider
    {
        public string[] Get(string username)
        {
            return new string[0];
        }
    }
}