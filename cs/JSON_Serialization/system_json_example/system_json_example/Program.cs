using system_json_example;
using System.Text.Json;
using System.Text.Json.Serialization;


// var json_content = File.ReadAllText("./current_jobCodes.json");

// List<JobCode> lstJobCodes = JsonSerializer.Deserialize<List<JobCode>>(json_content);

// Console.WriteLine($"Count of Job Codes: {lstJobCodes.Count}");

Role role = new Role()
{
    EmployeeID = "T00099833",
    RoleID = "113",
    UnitID = "019040",
    DescendHierarchy = null
};

Console.WriteLine(JsonSerializer.Serialize(role));


var json_content = File.ReadAllText("./datetime.json");

List<Role> lstRole = JsonSerializer.Deserialize<List<Role>>(json_content);

if (lstRole.Count() == 1)
{
    Console.WriteLine($"Active To: {lstRole[0].ActiveTo}");
    Console.WriteLine($"Active From: {lstRole[0].ActiveFrom}");
}

