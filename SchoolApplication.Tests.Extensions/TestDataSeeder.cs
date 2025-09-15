using SchoolApplication.Context;
using SchoolApplication.Entities;

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
        /// Возвращает существующую школы
        /// </summary>
        public async Task<School> SeedSchool(Action<School>? settings = null)
        {
            var school = TestDataGenerator.School(settings);

            context.Add(school);
            await context.SaveChangesAsync();

            return school;
        }

        /// <summary>
        /// Возвращает существующего родителя
        /// </summary>
        public async Task<Parent> SeedParent(Action<Parent>? settings = null)
        {
            var parent = TestDataGenerator.Parent(settings);

            context.Add(parent);
            await context.SaveChangesAsync();

            return parent;
        }

        /// <summary>
        /// Возвращает существующего ученика
        /// </summary>
        public async Task<Student> SeedStudent(Action<Student>? settings = null)
        {
            var student = TestDataGenerator.Student(settings);

            context.Add(student);
            await context.SaveChangesAsync();

            return student;
        }

        /// <summary>
        /// Возвращает айди существующего заявления
        /// </summary>
        public async Task<Application> SeedApplication(Action<Application>? settings = null)
        {
            var parent = await SeedParent();
            var student = await SeedStudent();
            var school = await SeedSchool();

            var application = TestDataGenerator.Application(settings);

            application.Parent = parent;
            application.ParentId = parent.Id;
            application.Student = student;
            application.StudentId = student.Id;
            application.School = school;
            application.SchoolId = school.Id;

            context.Add(application);
            await context.SaveChangesAsync();

            return application;
        }
    }
}
