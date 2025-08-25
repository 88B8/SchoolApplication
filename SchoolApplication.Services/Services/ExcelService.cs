using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SchoolApplication.Entities;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services
{
    /// <inheritdoc cref="IExcelService"/>
    public class ExcelService : IExcelService, IServiceAnchor
    {
        byte[] IExcelService.Export(Application application, CancellationToken cancellationToken)
        {
            using var stream = new MemoryStream();

            using (var spreadsheetDocument = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                var sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
                sheets.Append(new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Заявление"
                });

                uint rowIndex = 1;

                AddRow(sheetData, rowIndex++, "", $"Директору {application.School.Name}");
                AddRow(sheetData, rowIndex++, "", application.School.DirectorName);
                AddRow(sheetData, rowIndex++, "", $"от {application.Parent.Surname} {application.Parent.Name} {application.Parent.Patronymic}");
                AddRow(sheetData, rowIndex++, "");
                AddRow(sheetData, rowIndex++, "Заявление");
                AddRow(sheetData, rowIndex++, "");
                AddRow(sheetData, rowIndex++, $"Прошу Вас разрешить отсутствовать на учебных занятиях в школе с {application.DateFrom:dd.MM.yyyy}");
                AddRow(sheetData, rowIndex++, $"по {application.DateUntil:dd.MM.yyyy} моему {(application.Student.Gender == 0 ? "сыну" : "дочери")} {application.Student.Surname} {application.Student.Name} {application.Student.Patronymic}");
                AddRow(sheetData, rowIndex++, $"{(application.Student.Gender == 0 ? "ученика" : "ученицы")} {application.Student.Grade} класса {application.Reason}");

                workbookPart.Workbook.Save();
            }

            return stream.ToArray();
        }

        private void AddRow(SheetData sheetData, uint rowIndex, params string[] values)
        {
            var row = new Row() { RowIndex = rowIndex };

            foreach (var value in values)
            {
                var cell = new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(value)
                };
                row.Append(cell);
            }

            sheetData.Append(row);
        }
    }
}
