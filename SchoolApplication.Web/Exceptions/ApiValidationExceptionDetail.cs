using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Web
{
    /// <summary>
    /// Информация об ошибках валидации работы API
    /// </summary>
    public class ApiValidationExceptionDetail
    {
        /// <summary>
        /// Ошибки валидации
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; set; } = Array.Empty<InvalidateItemModel>();
    }
}
