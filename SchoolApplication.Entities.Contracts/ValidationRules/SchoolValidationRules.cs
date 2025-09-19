namespace SchoolApplication.Entities.Contracts.ValidationRules
{
    /// <summary>
    /// Правила валидации <see cref="School"/>
    /// </summary>
    public static class SchoolValidationRules
    {
        /// <summary>
        /// Минимальная длина <see cref="School.Name"/>
        /// </summary>
        public const int NameMinLength = 3;

        /// <summary>
        /// Максимальная длина <see cref="School.Name"/>
        /// </summary>
        public const int NameMaxLength = 255;

        /// <summary>
        /// Минимальная длина <see cref="School.DirectorName"/>
        /// </summary>
        public const int DirectorNameMinLength = 3;

        /// <summary>
        /// Максимальная длина <see cref="School.DirectorName"/>
        /// </summary>
        public const int DirectorNameMaxLength = 255;
    }
}
