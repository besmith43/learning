namespace datastore;

public interface IDataStore
{
	public string ConnectionString { get; set; }
	public void AddParameter(string parameterName, dynamic value, string type = "VarChar");
}