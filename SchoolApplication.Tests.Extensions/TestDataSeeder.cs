using SchoolApplication.Context;

namespace SchoolApplication.Tests.Extensions
{
    /// <summary>
    /// Генератор данных для интеграционных тестов
    /// </summary>
    public class TestDataSeeder
    {
        private readonly SchoolApplicationContext context;

        /// <summary>
        /// ctor
        /// </summary>
        public TestDataSeeder(SchoolApplicationContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Возвращает айди существующей школы
        /// </summary>
        public async Task<Guid> SeedSchool()
        {
            var school = TestDataGenerator.School();

            context.Add(school);
            await context.SaveChangesAsync();

            return school.Id;
        }

        /// <summary>
        /// Возвращает айди существующего родителя
        /// </summary>
        public async Task<Guid> SeedParent()
        {
            var parent = TestDataGenerator.Parent();

            context.Add(parent);
            await context.SaveChangesAsync();

            return parent.Id;
        }

        /// <summary>
        /// Возвращает айди существующего ученика
        /// </summary>
        public async Task<Guid> SeedStudent()
        {
            var student = TestDataGenerator.Student();

            context.Add(student);
            await context.SaveChangesAsync();

            return student.Id;
        }

        /// <summary>
        /// Возвращает айди существующего заявления
        /// </summary>
        public async Task<Guid> SeedApplication()
        {
            var application = TestDataGenerator.Application();

            context.Add(application);
            await context.SaveChangesAsync();

            return application.Id;
        }
    }
}
