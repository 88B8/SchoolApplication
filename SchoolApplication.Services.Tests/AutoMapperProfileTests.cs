using AutoMapper;

namespace SchoolApplication.Services.Tests
{
    /// <summary>
    /// Тесты профилей автомаппера
    /// </summary>
    public class AutoMapperProfileTests
    {
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public AutoMapperProfileTests()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<ServiceProfile>();
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
