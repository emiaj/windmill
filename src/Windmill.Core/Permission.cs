using System.Collections.Generic;
using FubuLocalization;

namespace Windmill.Core
{
    public class Permission
    {
        protected Permission()
        {
        }

        public Permission(string name)
            : this(name, null)
        {
        }

        public Permission(string name, string parent)
        {
            Parent = parent;
            Name = name;
            if (parent == null)
            {
                Id = name;
            }
            else
            {
                Id = parent + "/" + name;
            }
            AllowedGroups = new List<string>();
            AllowedUsers = new List<string>();
            ForbiddenGroups = new List<string>();
            ForbiddenUsers = new List<string>();
        }

        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public string Parent { get; protected set; }
        public IList<string> AllowedGroups { get; protected set; }
        public IList<string> AllowedUsers { get; protected set; }
        public IList<string> ForbiddenGroups { get; protected set; }
        public IList<string> ForbiddenUsers { get; protected set; }

        public string Title
        {
            get { return StringToken.FromKeyString("Permission:" + Id + ":Title").ToString(); }
        }

        public string Description
        {
            get { return StringToken.FromKeyString("Permission:" + Id + ":Description").ToString(); }
        }

        public override string ToString()
        {
            return Title;
        }
    }
}