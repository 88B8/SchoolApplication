namespace SchoolApplication.Services.Contracts
{
    /// <summary>
    /// Модель атрибута столбца Excel
    /// </summary>
    public class ExcelHeaderAttribute(string header, uint order) : Attribute
    {
        /// <summary>
        /// Название столбца
        /// </summary>
        public string Header { get; set; } = header;

        /// <summary>
        /// Порядковый номер столбца
        /// </summary>
        public uint Order { get; set; } = order;
    }
}