using DataStoreExample;

namespace DataStoreExample.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var dummy = new Dummy();

        var result = dummy.RunGet("SELECT * FROM DUAL");

        Assert.NotNull(result);
    }

    [Fact]
    public void Test2()
    {
        var dummy = new Dummy();

        var result = dummy.RunCreate("INSERT INTO DUAL VALUES ('X')");
        result = dummy.RunUpdate("UPDATE DUAL SET DUMMY = 'Y'");
        result = dummy.RunDelete("DELETE FROM DUAL");

        Assert.Equal(3, result);
    }

    [Fact]
    public void Test3()
    {
        var dummy = new Dummy();

        dummy.ConnectionString = "Data Source=//localhost:1521/xe;User Id=system;Password=oracle";

        Assert.Equal("Data Source=//localhost:1521/xe;User Id=system;Password=oracle", dummy.ConnectionString);
    }

    [Fact]
    public void Test4()
    {
        var dummy = new Dummy();

        dummy.AddParameter("Name", "Value");

        var parameters = dummy.GetParameters();

        Assert.Equal("Name", parameters[0]);
        Assert.Equal("Value", parameters[1]);
    }
}