namespace api.Models;


public class CurrentSession
{
	public string UserID { get; set; }
	public string ADUserID { get; set; }
	public List<string> ADGroups { get; set; }
	public string SessionID { get; set; }
	public DateTime SessionStartDateTime { get; set; }
	public string IPAddress { get; set; }
}