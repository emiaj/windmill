using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;

namespace Windmill.Core
{
    public class PermissionDescriptor
    {
        public string Id { get; protected set; }
        public string AllowedUsers { get; protected set; }
        public string AllowedGroups { get; protected set; }
        public string ForbiddenGroups { get; protected set; }
        public string ForbiddenUsers { get; protected set; }

        public PermissionDescriptor(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (id.IsEmpty()) throw new ArgumentException("name cannot be empty", id);
            Id = id;
            AllowedUsers = string.Empty;
            AllowedGroups = string.Empty;
            ForbiddenGroups = string.Empty;
            ForbiddenUsers = string.Empty;
        }

        public PermissionDescriptor SetAllowedUsers(params string[] users)
        {
            if (users == null) throw new ArgumentNullException("users");
            AllowedUsers = users.Join(",");
            return this;
        }

        public PermissionDescriptor SetAllowedGroups(params string[] groups)
        {
            if (groups == null) throw new ArgumentNullException("groups");
            AllowedGroups = groups.Join(",");
            return this;
        }

        public PermissionDescriptor SetForbiddenGroups(params string[] groups)
        {
            if (groups == null) throw new ArgumentNullException("groups");
            ForbiddenGroups = groups.Join(",");
            return this;
        }

        public PermissionDescriptor SetForbiddenUsers(params string[] users)
        {
            if (users == null) throw new ArgumentNullException("users");
            ForbiddenUsers = users.Join(",");
            return this;
        }

        public void Update(Permission permission)
        {
            AllowedUsers.Split(',').Where(StringExtensions.IsNotEmpty)
                .Each(permission.AllowedUsers.Fill);
            AllowedGroups.Split(',').Where(StringExtensions.IsNotEmpty)
                .Each(permission.AllowedGroups.Fill);
            ForbiddenGroups.Split(',').Where(StringExtensions.IsNotEmpty)
                .Each(permission.ForbiddenGroups.Fill);
            ForbiddenUsers.Split(',').Where(StringExtensions.IsNotEmpty)
                .Each(permission.ForbiddenUsers.Fill);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (PermissionDescriptor)) return false;
            return Equals((PermissionDescriptor) obj);
        }

        public bool Equals(PermissionDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}