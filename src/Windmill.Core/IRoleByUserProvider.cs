namespace Windmill.Core
{
    public interface IRoleByUserProvider
    {
        string[] Get(string username);
    }
}