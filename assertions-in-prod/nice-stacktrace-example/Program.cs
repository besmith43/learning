using Shouldly;
using System.Diagnostics;
// ben.Demystify is extending the System.Diagnostics library

Exception exception = null;


string word = "Apple";

word.ShouldBe("Apple");

try
{
    Console.Error.WriteLine("");
    Console.Error.WriteLine("Failed Assert");
    Console.Error.WriteLine("");
    word.ShouldBe("apple");
}
catch (Exception ex)
{
    exception = ex.Demystify();
}

Console.WriteLine();
Console.WriteLine("outputing the new exception");
Console.WriteLine(exception);
Console.WriteLine();


/*

BAD OUTPUT

Failed Assert


outputing the new exception
Shouldly.ShouldAssertException: word
    should be
"apple"
    but was
"Apple"
    difference
Difference     |  |                       
               | \|/                      
Index          | 0    1    2    3    4    
Expected Value | a    p    p    l    e    
Actual Value   | A    p    p    l    e    
Expected Code  | 97   112  112  108  101  
Actual Code    | 65   112  112  108  101  
   at Program.<Main>$(String[] args) in /Users/besmith/Developer/Personal/learning-cs/assertions-in-prod/nice-stacktrace-example/Program.cs:line 17


*/

