using CMS.Data.Access.Configuration;

namespace CMS.Data.Access.UnitTests.MockData
{
    internal class TestEntities : EntitiesContext
    {
        public TestEntities(IConfigurationManagerService configurationManager) 
            : base(configurationManager)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
