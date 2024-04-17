// using System.DirectoryServices.Protocols;
using Novell.Directory.Ldap;
using System.Net;
using System.Security;

var username = "blakesmith";
var adminUsername = "CN=blakesmith2,OU=Privileged 2 Accounts,OU=Faculty-Staff,OU=TTU Users,DC=tntech,DC=edu";
// var adminUsername = "blakesmith2";
var TargetServer = "one.tntech.edu";
// var TargetServer = "ldap://one.tntech.edu";
// var TargetServer = "ldap://149.149.5.21";


var password = GetPassword();

Console.WriteLine();


using (var connection = new LdapConnection { SecureSocketLayer = false })
{
	connection.Connect(TargetServer, 389);
	connection.Bind(adminUsername, password);
	if (connection.Bound)
	{
		Console.WriteLine("Successfully authenticated");
	}
	else
	{
		Console.WriteLine("Authentication failed");
	}

	var searchResponse = connection.Search(
		"dc=tntech,dc=edu",
		LdapConnection.ScopeSub,
		$"(&(SAMAccountName={username})(objectClass=user))",
		null,
		false
	);

	var entry = searchResponse.Next();

	Console.WriteLine(entry.Dn);
	Console.WriteLine(entry.GetAttribute("ExtensionAttribute2").StringValue);
	Console.WriteLine(entry.GetAttribute("memberOf"));

	string strGroups = "";

	foreach (var group in entry.GetAttribute("memberOf").StringValueArray)
	{
		Console.WriteLine(group);
        strGroups += group.Substring(3, group.IndexOf(",") - 3) + ",";
	}

	Console.WriteLine(strGroups);

	/*
	while(searchResponse.HasMore())
	{
		// other data needed
		// UDCID
		// group membership
		var entry = searchResponse.Next();

		Console.WriteLine(entry.Dn);
		Console.WriteLine(entry.GetAttribute("ExtensionAttribute2").StringValue);
		Console.WriteLine(entry.GetAttribute("memberOf"));

		foreach (var group in entry.GetAttribute("memberOf").StringValueArray)
		{
			Console.WriteLine(group);
		}
	}
	*/

	connection.Disconnect();
}


/*
using var connection = new LdapConnection("one.tntech.edu");

var networkCredential = new NetworkCredential(adminUsername, password);
connection.SessionOptions.SecureSocketLayer = false;
connection.SessionOptions.ProtocolVersion = 3;
connection.Timeout = new TimeSpan(0, 0, 30);
connection.AuthType = AuthType.Basic;
connection.Bind(networkCredential);

var searchRequest = new SearchRequest(
	"DC=tntech,DC=edu",
	$"(SAMAccountName={username})",
	SearchScope.OneLevel,
	new string[]
	{
		"cn",
		"memberOf",
		"employeeid",
		"telephonenumber",
		"displayName",
		"mail"
	});

SearchResponse directoryResponse = (SearchResponse)connection.SendRequest(searchRequest);

if (directoryResponse.Entries.Count > 0)
{
	SearchResultEntry searchResultEntry = directoryResponse.Entries[0];
	Console.WriteLine("Search Result: " + searchResultEntry);
}
else
{
	Console.WriteLine("The response was empty");
}
*/

/*
var samAccountNames = new List<string>();

using(var ldapConnection = new LdapConnection("tntech.edu:3268"))
{
	ldapConnection.AuthType = AuthType.Basic;
	ldapConnection.Credential = new NetworkCredential(@"tntech.edu\blakesmith2", password);

	var searchResponse = (SearchResponse)ldapConnection.SendRequest(new SearchRequest("DC=tntech,DC=edu", "sAMAccountName=blakesmith", SearchScope.Subtree, "sAMAccountName"));

	foreach(var entry in searchResponse.Entries.Cast<SearchResultEntry>())
	{
		var attribute = entry.Attributes["sAMAccountName"];

		samAccountNames.AddRange(attribute.GetValues(typeof(string)).Cast<string>());
	}
}

foreach(var samAccountName in samAccountNames)
{
	Console.WriteLine(samAccountName);
}
*/

/*
// Configure server and port. LDAP w/ SSL, aka LDAPS, uses port 636.
// If you don't have SSL, don't give it the SSL port. 
LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier(TargetServer);
// LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier(TargetServer, 389);
// LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier(TargetServer, true, false);

// Configure network credentials (userid and password)
NetworkCredential creds = new NetworkCredential(adminUsername, password);
// NetworkCredential creds = new NetworkCredential(adminUsername, password, "tntech.edu");

// Actually create the connection
LdapConnection connection = new LdapConnection(identifier, creds)
{
    AuthType = AuthType.Basic, 
    SessionOptions =
    {
        ProtocolVersion = 3,
        // SecureSocketLayer = true,
		ReferralChasing = ReferralChasingOptions.None
    },
	Timeout = new TimeSpan(0, 0, 30)
};

// Override SChannel reverse DNS lookup.
// This gets us past the "The LDAP server is unavailable." exception
// Could be 
//    connection.SessionOptions.VerifyServerCertificate += { return true; };
// but some certificate validation is probably good.
// connection.SessionOptions.VerifyServerCertificate +=
//     (sender, certificate) => certificate.Subject.Contains(string.Format("CN={0},", TargetServer));



SearchRequest searchRequest = new SearchRequest(
        "dc=tntech,dc=edu", 
        $"(&(SAMAccountName={username})(objectClass=user))", 
        SearchScope.Subtree,
        "*"
);

var searchResponse = (SearchResponse)connection.SendRequest(searchRequest);

// Look at your results
foreach (SearchResultEntry entry in searchResponse.Entries) {
    // do something
	Console.WriteLine(entry.DistinguishedName);
}
*/

/*
using (var cn = new LdapConnection(new LdapDirectoryIdentifier("one.tntech.edu"), new NetworkCredential(adminUsername, password)))
{
	cn.SessionOptions.ProtocolVersion = 3;
	cn.SessionOptions.SecureSocketLayer = true;  // this gets an error that the ldap server is unavailable
	cn.SessionOptions.VerifyServerCertificate += //{ return true; };
			(sender, certificate) => certificate.Subject.Contains(string.Format("CN={0},", "one.tntech.edu"));
	cn.AuthType = AuthType.Basic;
	cn.Bind();
	var request = new SearchRequest("dc=tntech,dc=edu", $"(&(SAMAccountName={username})(objectClass=user))", SearchScope.Subtree, "*");
	var response = (SearchResponse)cn.SendRequest(request);
	foreach (SearchResultEntry entry in response.Entries)
	{
		Console.WriteLine(entry.DistinguishedName);
	}
}
*/

string GetPassword()
{
	SecureString password = new SecureString();
	Console.WriteLine("Enter password: ");

	ConsoleKeyInfo nextKey = Console.ReadKey(true);

	while (nextKey.Key != ConsoleKey.Enter)
	{
		if (nextKey.Key == ConsoleKey.Backspace)
		{
			if (password.Length > 0)
			{
				password.RemoveAt(password.Length - 1);
				// erase the last * as well
				Console.Write(nextKey.KeyChar);
				Console.Write(" ");
				Console.Write(nextKey.KeyChar);
			}
		}
		else
		{
			password.AppendChar(nextKey.KeyChar);
			Console.Write("*");
		}
		nextKey = Console.ReadKey(true);
	}
	password.MakeReadOnly();

	return new NetworkCredential("", password).Password;
}