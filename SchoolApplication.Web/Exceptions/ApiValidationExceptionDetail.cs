using SchoolApplication.Services.Contracts.Exceptions;

namespace SchoolApplication.Web.Exceptions
{
    /// <summary>
    /// Информация об ошибках валидации работы API
    /// </summary>
    public class ApiValidationExceptionDetail
    {
        /// <summary>
        /// Ошибки валидации
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; set; } = [];
    }
}
