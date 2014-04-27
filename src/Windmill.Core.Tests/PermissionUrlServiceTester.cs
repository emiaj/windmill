using FubuMVC.Core.Urls;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace Windmill.Core.Tests
{
    [TestFixture]
    public class PermissionUrlServiceTester : InteractionContext<PermissionUrlService>
    {
        private IUrlRegistry _registry;
        private IPermissionContext _context;
        private string[] _permissions;
        private StubClass _model;
        private string _template1;
        private string _template2;

        protected override void beforeEach()
        {
            _model = new StubClass();
            _registry = MockFor<IUrlRegistry>();
            _context = MockFor<IPermissionContext>();
            _permissions = new[] { "/product/delete", "/product/remove" };
            _template1 = "/{bar}/{baz}";
            _template2 = "/{foo}/{bar}";
            _registry.Stub(x => x.TemplateFor<StubClass>()).Return(_template1);
            _registry.Stub(x => x.TemplateFor(_model)).Return(_template2);
        }

        public class StubClass
        {
        }

        [TestFixture]
        public class when_has_no_permissions : PermissionUrlServiceTester
        {
            [SetUp]
            public void when_has_no_permissions_SetUp()
            {
                _context.Stub(x => x.Check(_permissions)).Return(false);
            }

            [Test]
            public void generic_template_for_returns_null()
            {
                ClassUnderTest.TemplateFor<StubClass>(_permissions).ShouldBeNull();
            }

            [Test]
            public void template_for_model_returns_null()
            {
                ClassUnderTest.TemplateFor(_model, _permissions).ShouldBeNull();
            }
        }

        [TestFixture]
        public class when_has_permissions : PermissionUrlServiceTester
        {
            [SetUp]
            public void when_has_permissions_SetUp()
            {
                _context.Stub(x => x.Check(_permissions)).Return(true);
            }

            [Test]
            public void generic_template_for_returns_template()
            {
                ClassUnderTest.TemplateFor<StubClass>(_permissions).ShouldEqual(_template1);
            }

            [Test]
            public void template_for_model_returns_template()
            {
                ClassUnderTest.TemplateFor(_model, _permissions).ShouldEqual(_template2);
            }
        }
    }
}