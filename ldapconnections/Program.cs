using System;
using System.Security;
using System.Net;
// using System.DirectoryServices;
using LdapForNet;
using static LdapForNet.Native.Native;

var username = "blakesmith";


/*
// directory searcher is only available on windows
var ds = new DirectorySearcher("LDAP://tntech.edu");

ds.Filter = $"(&(SAMAccountName={username})(objectClass=user))";
ds.SearchScope = SearchScope.Subtree;
ds.PropertiesToLoad.Add("distunguishedName");
ds.PropertiesToLoad.Add("extensionAttribute2");
ds.PropertiesToLoad.Add("memberOf");


SearchResultCollection results = ds.FindAll();

foreach (SearchResult result in results)
{
	Console.WriteLine(result.Properties["distinguishedName"]);
	Console.WriteLine(result.Properties["extensionAttribute2"]);
	Console.WriteLine(result.Properties["memberOf"]);
}
*/


// works on ad connected windows machines
// does not work on an ad connected linux machine
// gets an error that the server can't be contacted due to a bad SASL error
using (var cn = new LdapConnection())
{
	cn.Timeout = new TimeSpan(0, 0, 30);
	cn.Connect();
	cn.Bind();
	var entries = cn.Search("dc=tntech,dc=edu", $"(&(SAMAccountName={username})(objectClass=user))");

	foreach (var entry in entries)
	{
		Console.WriteLine(entry.Dn);
	}
}


/*
var password = GetPassword();

Console.WriteLine();

using (var cn = new LdapConnection())
{
	cn.Timeout = new TimeSpan(0, 0, 30);
	cn.Connect("ldap://149.149.5.21", 389, LdapSchema.LDAPS, LdapVersion.LDAP_VERSION3);
	// cn.TrustAllCertificates();
	// cn.StartTransportLayerSecurity(true);
	cn.Bind(LdapAuthType.Simple, new LdapCredential {
		UserName = "CN=blakesmith2,OU=Privileged 2 Accounts,OU=Faculty-Staff,OU=TTU Users,DC=tntech,DC=edu",
		Password = password
	});
	var entries = cn.Search("dc=tntech,dc=edu", $"(&(SAMAccountName={username})(objectClass=user))");

	foreach (var entry in entries)
	{
		Console.WriteLine(entry.Dn);
	}
}



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
*/