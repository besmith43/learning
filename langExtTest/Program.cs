using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

var thingy1 = Some(1);

thingy1.Match(
	Some: v => Console.WriteLine($"Some: {v}"),
	None: () => Console.WriteLine("None")
);

var thingy2 = DoSomething("1");

var matchResultValue = thingy2.Match<string>(
	message => message,
	err => err.Message
);

Console.WriteLine(matchResultValue);

var thingy3 = DoSomething2(-1);

var matchResultValue2 = thingy3.Match<object>(
	message => message,
	err => err.Message
);

Console.WriteLine(matchResultValue2);


Result<string> DoSomething(string x)
{
	if (x == "1")
	{
		return new Result<string>(x);
	}
	else
	{
		return  new Result<string>(new Exception("x must be greater than 0"));
	}
}
	
Result<int> DoSomething2(int x)
{
	if (x > 0)
	{
		return new Result<int>(x);
	}
	else
	{
		return  new Result<int>(new Exception("x must be greater than 0"));
	}
}