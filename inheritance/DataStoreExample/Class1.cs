using System.Data;

namespace DataStoreExample;

public interface DataStore
{
    public DataSet RunGet(string strQuery);
    public int RunCreate(string strQuery);
    public int RunUpdate(string strQuery);
    public int RunDelete(string strQuery);
}


public interface DataStore<T>
{
    public List<T> RunGet(string strQuery);
    public int RunCreate(string strQuery);
    public int RunUpdate(string strQuery);
    public int RunDelete(string strQuery);
}

public abstract class DummyRoot
{
    public string ConnectionString { get; set; }
    private int count = 0;
    protected List<string> parameters = new List<string>();

    public int RunCreate(string strQuery)
    {
        count++;
        return count;
    }

    public int RunUpdate(string strQuery)
    {
        count++;
        return count;
    }

    public int RunDelete(string strQuery)
    {
        count++;
        return count;
    }

    public void AddParameter(string strName, string strValue)
    {
        parameters.Add(strName);
        parameters.Add(strValue);
    }

    public void ClearParameters()
    {
        parameters.Clear();
    }
}


public class Dummy<T> : DummyRoot, DataStore<T>
{
    public List<T> RunGet(string strQuery)
    {
        return new List<T>();
    }
}

public class Dummy : DummyRoot, DataStore
{
    public DataSet RunGet(string strQuery)
    {
        return new DataSet();
    }

    public List<string> GetParameters()
    {
        return parameters;
    }
}