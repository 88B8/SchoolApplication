using AutoMapper;

namespace SchoolApplication.Web.Tests
{
    /// <summary>
    /// Тесты профилей автомаппера
    /// </summary>
    public class ApiMapperProfileTests
    {
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public ApiMapperProfileTests()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<ApiMapper>();
            });

            mapper = config.CreateMapper();
        }

        /// <summary>
        /// Маппинг правильно сформирован
        /// </summary>
        [Fact]
        public void ValidateMapperConfiguration()
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
