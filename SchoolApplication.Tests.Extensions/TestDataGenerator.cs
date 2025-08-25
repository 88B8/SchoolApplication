using SchoolApplication.Entities;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Web;

namespace SchoolApplication.Tests.Extensions
{
    /// <summary>
    /// Генератор данных для тестов
    /// </summary>
    public static class TestDataGenerator
    {
        /// <summary>
        /// Создает <see cref="Entities.Application"/>
        /// </summary>
        public static Application Application(Action<Application>? settings = null)
        {
            var student = Student();
            var parent = Parent();
            var school = School();

            var result = new Application
            {
                StudentId = student.Id,
                Student = student,
                ParentId = parent.Id,
                Parent = parent,
                SchoolId = school.Id,
                School = school,
                Reason = "по семейным обстоятельствам",
                DateFrom = new DateTime(2025, 07, 13),
                DateUntil = new DateTime(2025, 07, 14),
            };

            result.SetBaseAuditData();
            settings?.Invoke(result);
            return result;
        }

        /// <summary>
        /// Создает <see cref="Entities.Parent"/>
        /// </summary>
        public static Parent Parent(Action<Parent>? settings = null)
        {
            var result = new Parent
            {
                Surname = "Иванов",
                Name = "Иван",
                Patronymic = "Иванович",
            };

            result.SetBaseAuditData();
            settings?.Invoke(result);
            return result;
        }

        /// <summary>
        /// Создает <see cref="Entities.School"/>
        /// </summary>
        public static School School(Action<School>? settings = null)
        {
            var result = new School
            {
                Name = "Школа №1",
                DirectorName = "Петров. П.П.",
            };

            result.SetBaseAuditData();
            settings?.Invoke(result);
            return result;
        }

        /// <summary>
        /// Создает <see cref="Entities.Student"/>
        /// </summary>
        public static Student Student(Action<Student>? settings = null)
        {
            var result = new Student
            {
                Gender = Gender.Male,
                Surname = "Петров",
                Name = "Петр",
                Patronymic = "Петрович",
                Grade = "9А",
            };

            result.SetBaseAuditData();
            settings?.Invoke(result);
            return result;
        }

        /// <summary>
        /// Создает <see cref="Services.Contracts.ApplicationCreateModel"/>
        /// </summary>
        public static ApplicationCreateModel ApplicationCreateModel(Action<ApplicationCreateModel>? settings = null)
        {
            var result = new ApplicationCreateModel
            {
                StudentId = Guid.NewGuid(),
                ParentId = Guid.NewGuid(),
                SchoolId = Guid.NewGuid(),
                Reason = "по семейным обстоятельствам",
                DateFrom = new DateTime(2025, 07, 13),
                DateUntil = new DateTime(2025, 07, 14),
            };

            settings?.Invoke(result);
            return result;
        }

        /// <summary>
        /// Создает <see cref="Services.Contracts.ParentCreateModel"/>
        /// </summary>
        public static ParentCreateModel ParentCreateModel(Action<ParentCreateModel>? settings = null)
        {
            var result = new ParentCreateModel
            {
                Surname = "Иванов",
                Name = "Иван",
                Patronymic = "Иванович",
            };

            settings?.Invoke(result);
            return result;
        }

        /// <summary>
        /// Создает <see cref="Services.Contracts.SchoolCreateModel"/>
        /// </summary>
        public static SchoolCreateModel SchoolCreateModel(Action<SchoolCreateModel>? settings = null)
        {
            var result = new SchoolCreateModel
            {
                Name = "Школа №1",
                DirectorName = "Петров П.П.",
            };

            settings?.Invoke(result);
            return result;
        }

        /// <summary>
        /// Создает <see cref="Services.Contracts.StudentCreateModel"/>
        /// </summary>
        public static StudentCreateModel StudentCreateModel(Action<StudentCreateModel>? settings = null)
        {
            var result = new StudentCreateModel
            {
                Gender = GenderModel.Male,
                Surname = "Петров",
                Name = "Петр",
                Patronymic = "Петрович",
                Grade = "9А",
            };

            settings?.Invoke(result);
            return result;
        }

        /// <summary>
        /// Создает <see cref="Services.Contracts.ApplicationModel"/>
        /// </summary>
        public static ApplicationModel ApplicationModel(Action<ApplicationModel>? settings = null)
        {
            var student = StudentModel();
            var parent = ParentModel();
            var school = SchoolModel();
            var result = new ApplicationModel
            {
                Id = Guid.NewGuid(),
                StudentId = student.Id,
                ParentId = parent.Id,
                SchoolId = school.Id,
                Reason = "по семейным обстоятельствам",
                DateFrom = new DateTime(2025, 07, 13),
                DateUntil = new DateTime(2025, 07, 14),
            };

            settings?.Invoke(result);
            return result;
        }

        /// <summary>
        /// Создает <see cref="Services.Contracts.ParentModel"/>
        /// </summary>
        public static ParentModel ParentModel(Action<ParentModel>? settings = null)
        {
            var result = new ParentModel
            {
                Id = Guid.NewGuid(),
                Surname = "Петров",
                Name = "Петр",
                Patronymic = "Петрович",
            };

            settings?.Invoke(result);
            return result;
        }

        /// <summary>
        /// Создает <see cref="Services.Contracts.SchoolModel"/>
        /// </summary>
        public static SchoolModel SchoolModel(Action<SchoolModel>? settings = null)
        {
            var result = new SchoolModel
            {
                Id = Guid.NewGuid(),
                Name = "Школа №1",
                DirectorName = "Петров П.П.",
            };

            settings?.Invoke(result);
            return result;
        }

        /// <summary>
        /// Создает <see cref="Services.Contracts.ApplicationModel"/>
        /// </summary>
        public static StudentModel StudentModel(Action<StudentModel>? settings = null)
        {
            var result = new StudentModel
            {
                Id = Guid.NewGuid(),
                Gender = GenderModel.Male,
                Surname = "Иванов",
                Name = "Иван",
                Patronymic = "Иванович",
                Grade = "9А",
            };

            settings?.Invoke(result);
            return result;
        }
    }
}