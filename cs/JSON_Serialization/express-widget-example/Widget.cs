using System.Text.Json.Serialization;

namespace Express;

public class Widget
{
	public string WidgetID { get; set; }
	public string ReadableTitle { get; set; }
	public DateTime InstallDateTime { get; set; }
	public string DeveloperName { get; set; }
	public string DeveloperAddress { get; set; }
	public string DeveloperPhone { get; set; }
	public string DeveloperEmail { get; set; }
	public string Version { get; set; }
	public string? UserPermissions { get; set; }
	public string? GroupPermissions { get; set; }
	public string? ClientSideLoad { get; set; }
	public string? ServerSideLoad { get; set; }
	public string? InstallSource { get; set; }
	public string? InitialClientSideCalls { get; set; }
	public string? InitialServerSideCalls { get; set; }
	public string? ADGroupPermissions { get; set; }
	// public string Status { get; set; } // note that this should only be a value of "Active" or "Archived"
	public WidgetStatus Status { get; set; }
	public DateTime? LastUpdateDateTime { get; set; }
	public bool Removable { get; set; }
	public string? Description { get; set; }
	public bool? VPNRequired { get; set; }
	public string? Type { get; set; }
	public string? ParentCard { get; set; }
	public int? TabOrder { get; set; }
	public bool LoadInitially { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WidgetStatus
{
	Active,
	Archived
}