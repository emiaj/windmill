namespace Windmill.Core.Configuration
{
    public class PermissionToken
    {
        public PermissionToken(string permissionId)
        {
            Id = permissionId;
        }

        public string Id { get; set; }
    }
}