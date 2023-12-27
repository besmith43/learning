using system_json_example;
using System.Text.Json;
using System.Text.Json.Serialization;
using OneOf;

// var json_content = File.ReadAllText("./current_jobCodes.json");

// List<JobCode> lstJobCodes = JsonSerializer.Deserialize<List<JobCode>>(json_content);

// Console.WriteLine($"Count of Job Codes: {lstJobCodes.Count}");

// Role role = new Role()
// {
//     EmployeeID = "T00099833",
//     RoleID = "113",
//     UnitID = "019040",
//     DescendHierarchy = null
// };

// Console.WriteLine(JsonSerializer.Serialize(role));


// var json_content = File.ReadAllText("./datetime.json");

// List<Role> lstRole = JsonSerializer.Deserialize<List<Role>>(json_content);

// if (lstRole.Count() == 1)
// {
//     Console.WriteLine($"Active To: {lstRole[0].ActiveTo}");
//     Console.WriteLine($"Active From: {lstRole[0].ActiveFrom}");
// }


var test_emp = new Employee()
{
    FirstName = "John",
    LastName = "Doe",
    TNumber = "T00099833",
    Address = "123 Main St",
    Department = "IT"
};


var person_json = JsonSerializer.Serialize(test_emp);

Console.WriteLine(person_json);

// this deserialize T type exclusively determines the type that you're getting back regardless of the content in the json object
var person = JsonSerializer.Deserialize<Employee>(person_json);

Console.WriteLine($"Person Type: {person.GetType()}");


Student student = new Student()
{
    FirstName = "Jane",
    LastName = "Doe",
    TNumber = "T00099833",
    Address = "123 Main St",
    Major = "Computer Science"
};

var student_json = JsonSerializer.Serialize(student);


Person person2;

if (student_json.Contains("Department"))
{
    person2 = JsonSerializer.Deserialize<Employee>(student_json);
}
else if (student_json.Contains("Major"))
{
    person2 = JsonSerializer.Deserialize<Student>(student_json);
}
else
{
    person2 = JsonSerializer.Deserialize<Person>(student_json);
}

Console.WriteLine($"Person2 Type: {person2.GetType()}");


public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TNumber { get; set; }
    public string Address { get; set; }
}

public class Employee : Person
{
    public string Department { get; set; }
}

public class Student : Person
{
    public string Major { get; set; }
}


