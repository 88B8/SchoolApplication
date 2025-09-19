namespace SchoolApplication.Web.Tests.Infrastructure
{
    /// <summary>
    /// Коллекция тестов
    /// </summary>
    [CollectionDefinition(nameof(SchoolApplicationCollection))]
    public class SchoolApplicationCollection : ICollectionFixture<SchoolApplicationApiFixture>
    {

    }
}
