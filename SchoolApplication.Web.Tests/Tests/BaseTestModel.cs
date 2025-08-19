using AutoMapper;
using SchoolApplication.Context;
using SchoolApplication.Tests.Extensions;

namespace SchoolApplication.Web.Tests
{
    /// <summary>
    /// Базовый класс для тестов
    /// </summary>
    [Collection(nameof(SchoolApplicationCollection))]
    public class BaseTestModel
    {
        protected readonly ISchoolApplicationApiClient webClient;
        protected readonly SchoolApplicationContext context;
        protected readonly TestDataSeeder seeder;
        protected readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public BaseTestModel(SchoolApplicationApiFixture fixture)
        {
            webClient = fixture.WebClient;
            context = fixture.Context;
            seeder = new TestDataSeeder(context);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApiMapper>();
            });

            mapper = config.CreateMapper();
        }
    }
}
