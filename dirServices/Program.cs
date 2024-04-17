using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;

var username = "blakesmith";

UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(
				   new PrincipalContext(ContextType.Domain), username);

Console.WriteLine("User: {0}", userPrincipal.DistinguishedName);
