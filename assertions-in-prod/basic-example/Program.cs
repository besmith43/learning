using Shouldly;


string word = "Apple";

word.ShouldBe("Apple");
word.ShouldBe("apple");
// program stops here on assert failure

DoWork().ShouldBe("Work");
DoWork().ShouldBe("Working");

/*

BAD OUTPUT

if the assert passes than you get nothing
it also works in release mode

Unhandled exception. Shouldly.ShouldAssertException: DoWork()
    should be
"Working"
    but was
"Work"
    difference
Difference     |                      |    |    |   
               |                     \|/  \|/  \|/  
Index          | 0    1    2    3    4    5    6    
Expected Value | W    o    r    k    i    n    g    
Actual Value   | W    o    r    k                   
Expected Code  | 87   111  114  107  105  110  103  
Actual Code    | 87   111  114  107                 
   at Program.<Main>$(String[] args) in /Users/besmith/Developer/Personal/learning-cs/assertions-in-prod/basic-example/Program.cs:line 5

*/

string DoWork()
{
    return "Work";
}

