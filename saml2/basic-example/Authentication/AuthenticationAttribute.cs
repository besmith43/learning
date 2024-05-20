using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using api.Models;
using api.Utils;
using OneOf;
// using DataStore;

namespace api.Authentication;

public class AuthenticationAttribute : Attribute, IAuthorizationFilter
{
	private readonly ILogger<AuthenticationAttribute> _logger;

	public AuthenticationAttribute(ILogger<AuthenticationAttribute> logger)
	{
		_logger = logger;

		_logger.LogInformation("AuthenticationAttribute: Initialized");
	}


	public void OnAuthorization(AuthorizationFilterContext context)
	{
		_logger.LogInformation("Checking Authentication");

		if (context.HttpContext.Request.Headers.ContainsKey(AuthenticationConstants.AuthAPIKeyHeader))
		{
			string apiKey = context.HttpContext.Request.Headers[AuthenticationConstants.AuthAPIKeyHeader];

			_logger.LogInformation("Checking API Key: " + apiKey);

			var result = checkAPIKey(apiKey);

			if (result)
			{
				context.HttpContext.Items["APIKey"] = apiKey;
			}
			else
			{
				_logger.LogInformation("Unauthorized");
				context.Result = new UnauthorizedResult();
				return;
			}
		}
		else if (context.HttpContext.Request.Headers.ContainsKey(AuthenticationConstants.AuthSessionIDHeader))
		{
			string sessionID = context.HttpContext.Request.Headers[AuthenticationConstants.AuthSessionIDHeader];

			var result = checkAccess(sessionID);

			if (result is not null)
			{
				context.HttpContext.Items["CurrentSession"] = result;
			}
			else
			{
				_logger.LogInformation("Unauthorized");
				context.Result = new UnauthorizedResult();
				return;
			}
		}
		else
		{
			_logger.LogInformation("Didn't pass in an api key or session id");
			context.Result = new UnauthorizedResult();
			return;
		}
	}


	private CurrentSession? checkAccess(string strSessionID)
	{
		foreach (var session in SharedData.lstCurrentSessions)
		{
			if (session.SessionID == strSessionID)
			{
				return session;
			}
		}

		return null;
	}

	private bool checkAPIKey(string strAPIKey)
	{
		foreach (var key in SharedData.lstApiKeys)
		{
			if (key.key == strAPIKey)
			{
				return true;
			}
		}

		return false;
	}
}