using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Authorization;

public class AuthorizationAttribute : TypeFilterAttribute
{
	public AuthorizationAttribute(string WidgetID, string FunctionID = "", bool APIKeysAllowed = false) : base(typeof(AuthorizationFilter))
	{
		Arguments = new object[] {
			WidgetID,
			FunctionID,
			APIKeysAllowed
		};
	}
}