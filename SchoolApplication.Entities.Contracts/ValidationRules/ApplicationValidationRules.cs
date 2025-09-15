namespace SchoolApplication.Entities.Contracts.ValidationRules
{
    /// <summary>
    /// Правила валидации <see cref="Application"/>
    /// </summary>
    public static class ApplicationValidationRules
    {
        /// <summary>
        /// Минимальная длина <see cref="Application.Reason"/>
        /// </summary>
        public const int ReasonMinLength = 3;

        /// <summary>
        /// Максимальная длина <see cref="Application.Reason"/>
        /// </summary>
        public const int ReasonMaxLength = 255;
    }
}
