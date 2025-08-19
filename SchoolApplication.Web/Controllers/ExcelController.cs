using Microsoft.AspNetCore.Mvc;
using SchoolApplication.Services.Contracts.Services;

namespace SchoolApplication.Web
{
    /// <summary>
    /// CRUD контроллер по работе с экспортом и импортом Excel файлов
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelDocumentService excelDocService;

        /// <summary>
        /// ctor
        /// </summary>
        public ExcelController(IExcelDocumentService excelDocService)
        {
            this.excelDocService = excelDocService;
        }

        /// <summary>
        /// Экспортирует данные в файл
        /// </summary>
        /// GET: /Application/
        [HttpGet]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        public async Task<IActionResult> Export()
        {
            var file = await excelDocService.ExportFile(CancellationToken.None);

            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "export.xlsx");
        }

        /// <summary>
        /// Импортирует данные из файла
        /// </summary>
        /// POST: /Application/
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var result = await excelDocService.ImportFromFile(stream, CancellationToken.None);

            return Ok(result);
        }
    }
}