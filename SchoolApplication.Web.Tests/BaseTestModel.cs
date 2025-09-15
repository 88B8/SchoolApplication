using SchoolApplication.Context;
using SchoolApplication.Tests.Extensions;
using SchoolApplication.Web.Tests.Client;
using SchoolApplication.Web.Tests.Infrastructure;

namespace SchoolApplication.Web.Tests
{
    /// <summary>
    /// Базовый класс для тестов
    /// </summary>
    [Collection(nameof(SchoolApplicationCollection))]
    public class BaseTestModel
    {
        protected ISchoolApplicationApiClient WebClient { get; }
        protected SchoolApplicationContext Context { get; }
        protected TestDataSeeder Seeder { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public BaseTestModel(SchoolApplicationApiFixture fixture)
        {
            WebClient = fixture.WebClient;
            Context = fixture.Context;
            Seeder = new TestDataSeeder(Context);
        }
    }
}
