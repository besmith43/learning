namespace api.Models;

public class ApiKey
{
	public string key { get; set; }
	public List<string> ADGroups { get; set; }
	public DateTime ExpirationDate { get; set; }
}