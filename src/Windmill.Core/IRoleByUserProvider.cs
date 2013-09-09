namespace Windmill.Core
{
    public interface IRoleByUserProvider
    {
        string[] GetRolesForUser(string username);
    }
}