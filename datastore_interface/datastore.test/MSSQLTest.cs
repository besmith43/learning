using datastore;

namespace datastore.test;

public class MSSQLTest
{
    [Fact]
    public void MSSQL_Paramters_Test()
    {
        MSSQL db = new MSSQL();

        db.AddParameter("string test", "string value");
        db.AddParameter("int test", 8, "Int");
        db.AddParameter("datetime test", new DateOnly(2024, 08, 28), "Date");

        Assert.Equal(3, db.parameters.Count);
    }
}