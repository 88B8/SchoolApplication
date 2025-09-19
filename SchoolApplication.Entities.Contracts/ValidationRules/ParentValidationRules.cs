namespace SchoolApplication.Entities.Contracts.ValidationRules
{
    /// <summary>
    /// Правила валидации <see cref="Parent"/>
    /// </summary>
    public static class ParentValidationRules
    {
        /// <summary>
        /// Минимальная длина <see cref="Parent.Surname"/>
        /// </summary>
        public const int SurnameMinLength = 2;

        /// <summary>
        /// Максимальная длина <see cref="Parent.Surname"/>
        /// </summary>
        public const int SurnameMaxLength = 255;

        /// <summary>
        /// Минимальная длина <see cref="Parent.Name"/>
        /// </summary>
        public const int NameMinLength = 2;

        /// <summary>
        /// Максимальная длина <see cref="Parent.Name"/>
        /// </summary>
        public const int NameMaxLength = 255;

        /// <summary>
        /// Минимальная длина <see cref="Parent.Patronymic"/>
        /// </summary>
        public const int PatronymicMinLength = 2;

        /// <summary>
        /// Максимальная длина <see cref="Parent.Patronymic"/>
        /// </summary>
        public const int PatronymicMaxLength = 255;
    }
}
