namespace SchoolApplication.Entities.Contracts.ValidationRules
{
    /// <summary>
    /// Правила валидации <see cref="Student"/>
    /// </summary>
    public static class StudentValidationRules
    {
        /// <summary>
        /// Минимальная длина <see cref="Student.Surname"/>
        /// </summary>
        public const int SurnameMinLength = 2;

        /// <summary>
        /// Максимальная длина <see cref="Student.Surname"/>
        /// </summary>
        public const int SurnameMaxLength = 255;

        /// <summary>
        /// Минимальная длина <see cref="Student.Name"/>
        /// </summary>
        public const int NameMinLength = 2;

        /// <summary>
        /// Максимальная длина <see cref="Student.Name"/>
        /// </summary>
        public const int NameMaxLength = 255;

        /// <summary>
        /// Минимальная длина <see cref="Student.Patronymic"/>
        /// </summary>
        public const int PatronymicMinLength = 2;

        /// <summary>
        /// Максимальная длина <see cref="Student.Patronymic"/>
        /// </summary>
        public const int PatronymicMaxLength = 255;

        /// <summary>
        /// Минимальная длина <see cref="Student.Grade"/>
        /// </summary>
        public const int GradeMinLength = 2;

        /// <summary>
        /// Максимальная длина <see cref="Student.Grade"/>
        /// </summary>
        public const int GradeMaxLength = 3;
    }
}
