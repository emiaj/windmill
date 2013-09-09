using System.Web.Security;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace Windmill.Core.Tests
{
    [TestFixture]
    public class RoleByUserProviderTester : InteractionContext<RoleByUserProvider>
    {
        private string[] _roles;
        private string _username;

        protected override void beforeEach()
        {
            _roles = new[] { "bar", "baz" };
            _username = "foo";
            MockFor<RoleProvider>().Stub(x => x.GetRolesForUser(_username)).Return(_roles);
        }

        [Test]
        public void get_user_by_roles_forwards_to_role_provider_dependency()
        {
            ClassUnderTest.Get(_username).ShouldEqual(_roles);
        }
    }
}
