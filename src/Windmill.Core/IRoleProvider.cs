namespace Windmill.Core
{
    public interface IRoleProvider
    {
        string[] GetRolesForUser(string username);
    }
}