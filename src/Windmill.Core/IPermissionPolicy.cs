namespace Windmill.Core
{
    public interface IPermissionPolicy
    {
        void Apply(IPermissionRegistryCache cache);
    }
}