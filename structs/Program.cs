using System;

namespace structs
{
	struct Employee
	{
		    public int EmpId;
		    public string FirstName;
		    public string LastName;

		    static Employee()
		    {
		        Console.Write("First object created");
		    }

		    public Employee(int empid, string fname, string lname)
		    {
				EmpId = empid;
				FirstName = fname;
				LastName = lname;
		    }
	}

    class Program
    {
        static void Main(string[] args)
        {
			Employee emp1 = new Employee(10, "Bill", "Gates");
			Employee emp2 = new Employee(10, "Steve", "Jobs");
        }
    }
}
