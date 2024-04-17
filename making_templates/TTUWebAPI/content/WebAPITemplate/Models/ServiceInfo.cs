namespace WebAPITemplate.Models;

public class ServiceInfo
{
	public string IPAddress { get; set; }
	public string HostName { get; set; }
	public string OS { get; set; }
	public string OSVersion { get; set; }
	public string OSArchitecture { get; set; }
	public string ASPNETCoreVersion { get; set; }

	public ServiceInfo()
	{
		IPAddress = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
		HostName = System.Environment.MachineName;
		OS = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
		OSVersion = System.Environment.OSVersion.VersionString;
		OSArchitecture = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString();
		ASPNETCoreVersion = System.Environment.Version.ToString();
	}
}
