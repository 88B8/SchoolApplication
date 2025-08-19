using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Services.Contracts.Services;
using System.Reflection;

namespace SchoolApplication.Services.Services
{
    /// <inheritdoc cref="IExcelService"/>
    public class ExcelService : IExcelService, IServiceAnchor
    {
        public byte[] Export(Dictionary<string, IEnumerable<object>> sheets, CancellationToken cancellationToken)
        {
            using var mem = new MemoryStream();
            using (var document = SpreadsheetDocument.Create(mem, SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                var sheetsCollection = workbookPart.Workbook.AppendChild(new Sheets());

                uint sheetId = 1;

                foreach (var sheetEntry in sheets)
                {
                    var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    var data = sheetEntry.Value;
                    var type = data.FirstOrDefault()?.GetType();

                    if (type == null)
                    {
                        continue;
                    }

                    var props = type.GetProperties()
                        .Select(p => new
                        {
                            Property = p,
                            Attribute = p.GetCustomAttribute<ExcelHeaderAttribute>(),
                        })
                        .Where(x => x.Attribute != null)
                        .OrderBy(x => x.Attribute.Order)
                        .ToList();

                    var sheetData = new SheetData();
                    var headerRow = new Row();

                    foreach (var col in props)
                    {
                        headerRow.Append(new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(col.Attribute.Header),
                        });
                    }

                    sheetData.Append(headerRow);

                    foreach (var item in data)
                    {
                        var row = new Row();

                        foreach (var col in props)
                        {
                            var value = col.Property.GetValue(item)?.ToString();

                            row.Append(new Cell
                            {
                                DataType = CellValues.String,
                                CellValue = new CellValue(value),
                            });
                        }

                        sheetData.Append(row);
                    }

                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    var sheet = new Sheet
                    {
                        Id = workbookPart.GetIdOfPart(worksheetPart),
                        SheetId = sheetId++,
                        Name = sheetEntry.Key,
                    };
                    sheetsCollection.Append(sheet);
                    workbookPart.Workbook.Save();
                }
            }

            return mem.ToArray();
        }

        public IEnumerable<T> Import<T>(Stream fileStream, string sheetName, CancellationToken cancellationToken) where T : class, new()
        {
            using var doc = SpreadsheetDocument.Open(fileStream, false);
            var workbookPart = doc.WorkbookPart;
            var sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(x => x.Name == sheetName);

            if (sheet == null)
            {
                throw new SchoolApplicationNotFoundException($"Лист не найден: {sheetName}");
            }

            var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
            var rows = worksheetPart.Worksheet.GetFirstChild<SheetData>().Elements<Row>().ToList();

            var props = typeof(T).GetProperties()
                .Select(p => new
                {
                    Property = p,
                    Attribute = p.GetCustomAttribute<ExcelHeaderAttribute>(),
                })
                .Where(x => x.Attribute != null)
                .ToList();

            var headerCells = rows[0].Elements<Cell>().Select(c => GetValue(c, workbookPart)).ToList();
            var map = new Dictionary<int, PropertyInfo>();

            for (int i = 0; i < headerCells.Count; i++)
            {
                var match = props.FirstOrDefault(x => x.Attribute.Header == headerCells[i]);
                if (match != null)
                    map[i] = match.Property;
            }

            var newEntities = new List<T>();

            foreach (var row in rows.Skip(1))
            {
                var obj = new T();
                var cells = row.Elements<Cell>().ToList();

                for (int i = 0; i < cells.Count; i++)
                {
                    if (map.TryGetValue(i, out var prop))
                    {
                        var cellValue = GetValue(cells[i], workbookPart);
                        if (cellValue != null)
                        {
                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            object? converted;

                            if (targetType == typeof(Guid))
                            {
                                converted = Guid.Parse(cellValue);
                            }
                            else if (targetType.IsEnum)
                            {
                                converted = Enum.Parse(targetType, cellValue);
                            }
                            else
                            {
                                converted = Convert.ChangeType(cellValue, targetType);
                            }

                            prop.SetValue(obj, converted); prop.SetValue(obj, converted);
                        }
                    }
                }

                newEntities.Add(obj);
            }

            return newEntities;
        }

        private static string? GetValue(Cell cell, WorkbookPart workbookPart)
        {
            if (cell == null || cell.CellValue == null)
                return null;

            string value = cell.CellValue.InnerText;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                var sst = workbookPart.SharedStringTablePart?.SharedStringTable;
                if (sst == null)
                    return value;

                if (int.TryParse(value, out int index))
                {
                    var item = sst.Elements<SharedStringItem>().ElementAtOrDefault(index);
                    return item?.InnerText ?? value;
                }
            }

            return value;
        }
    }
}