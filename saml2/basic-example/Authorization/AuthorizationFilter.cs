using System.Net;
using api.Models;
using api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OneOf;

namespace api.Authorization;

public class AuthorizationFilter : IAuthorizationFilter
{
	private readonly string _widgetID;
	private readonly string _functionID;
	private readonly bool _APIKeysAllowed;
	private readonly ILogger<AuthorizationFilter> _logger;

	public AuthorizationFilter(string widgetID, string FunctionID, bool APIKeysAllowed, ILogger<AuthorizationFilter> logger)
	{
		_widgetID = widgetID;
		_functionID = FunctionID;
		_APIKeysAllowed = APIKeysAllowed;
		_logger = logger;

		_logger.LogInformation($"AuthorizationFilter: WidgetID: {_widgetID}, FunctionID: {_functionID}, APIKeysAllowed: {_APIKeysAllowed}");
	}

	public void OnAuthorization(AuthorizationFilterContext context)
	{
		_logger.LogInformation("Checking Authorization");
/*
		if (!_APIKeysAllowed && context.HttpContext.Items.ContainsKey("APIKey"))
		{
			_logger.LogInformation("APIKeys are not allowed for this endpoint but an APIKey was provided.");
			context.Result = new BadRequestResult();
		}
		else if (_APIKeysAllowed && !string.IsNullOrEmpty(_functionID) && context.HttpContext.Items.ContainsKey("APIKey"))
		{
			_logger.LogInformation($"Checking APIKey Authorization for Function: {_functionID}");

			var apiKey = context.HttpContext.Items["APIKey"] as string;

			var result = checkFunctionAPIKeyAuth(apiKey);

			if (result.IsT1)
			{
				ErrorMessage errorMessage = result.AsT1;

				if (errorMessage.StatusCode == HttpStatusCode.Unauthorized)
				{
					_logger.LogInformation("Unauthorized: " + errorMessage.Message);
					context.Result = new UnauthorizedResult();
				}
				else if (errorMessage.StatusCode == HttpStatusCode.InternalServerError)
				{
					_logger.LogError("Internal Server Error: " + errorMessage.Message);
					context.Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError);
				}
				else
				{
					_logger.LogInformation("Unauthorized: " + errorMessage.Message);
					context.Result = new UnauthorizedResult();
				}
				return;
			}

			_logger.LogInformation("APIKey Authorized");
		}
		else if (_widgetID is not null && !context.HttpContext.Items.ContainsKey("APIKey"))
		{
			if (context.HttpContext.Items.ContainsKey("CurrentSession"))
			{
				_logger.LogInformation($"Checking Session ID Authorization for Widget: {_widgetID}");

				var curSession = context.HttpContext.Items["CurrentSession"] as CurrentSession;

				var result = checkWidgetAuthorization(curSession);

				if (result.IsT1)
				{
					ErrorMessage errorMessage = result.AsT1;

					if (errorMessage.StatusCode == HttpStatusCode.Unauthorized)
					{
						_logger.LogInformation("Unauthorized: " + errorMessage.Message);
						context.Result = new UnauthorizedResult();
					}
					else if (errorMessage.StatusCode == HttpStatusCode.InternalServerError)
					{
						_logger.LogError("Internal Server Error: " + errorMessage.Message);
						context.Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError);
					}
					else
					{
						_logger.LogInformation("Unauthorized: " + errorMessage.Message);
						context.Result = new UnauthorizedResult();
					}
					return;
				}

				_logger.LogInformation("Session ID Authorized");
			}
			else
			{
				context.Result = new UnauthorizedResult();
			}
		}*/
	}
/*
	// TODO: I think the challenge here is to get the widgetID based on the controller to be used
	private OneOf<bool, ErrorMessage> checkWidgetAuthorization(CurrentSession curSession)
	{
		var dataStoreWidgets = DataStoreFactory.GetExpressDS();
		dataStoreWidgets.AddParameter("@WidgetID", _widgetID);
		var resultWidgets = dataStoreWidgets.RunGet(
			@"SELECT * FROM express.tblWidgets WHERE WidgetID = @WidgetID"
		);

		// failure to connect to db
		if (resultWidgets.IsT1)
		{
			return new ErrorMessage
			{
				Message = resultWidgets.AsT1.Message,
				StackTrace = resultWidgets.AsT1.StackTrace,
				StatusCode = HttpStatusCode.InternalServerError
			};
		}

		var dsWidgets = resultWidgets.AsT0;

		// failure to find widget
		if (dsWidgets.Tables[0].Rows.Count == 0)
		{
			return new ErrorMessage
			{
				Message = "No Widgets found.",
				StackTrace = "",
				StatusCode = HttpStatusCode.BadRequest
			};
		}

		var strGroupPermissions = dsWidgets.Tables[0].Rows[0]["GroupPermissions"].ToString();
		var strUserPermissions = dsWidgets.Tables[0].Rows[0]["UserPermissions"].ToString();

		if (!string.IsNullOrEmpty(strUserPermissions))
		{
			foreach (string strUser in strUserPermissions.Split(','))
			{
				if (strUser == curSession.TNumber)
				{
					return true;
				}
			}
		}

		if (!string.IsNullOrEmpty(strGroupPermissions))
		{
			foreach (string strGroup in strGroupPermissions.Split(','))
			{
				if (curSession.ADGroups.Contains(strGroup))
				{
					return true;
				}
			}
		}

		return true;
	}

	private OneOf<bool, ErrorMessage> checkFunctionAPIKeyAuth(string apiKey)
	{
		string strQuery = "SELECT tblIntegrationMethods.FunctionCall, tblIntegrationMethods.Status AS MethodStatus, tblIntegrationKeys.IntegrationID, tblIntegrationKeys.Application, tblIntegrationKeys.LastCycled, tblIntegrationKeys.Status AS KeyStatus, tblIntegrationKeys.CurrentKey, tblIntegrationKeys.AccessAllowedTo, DATEADD(day,ExpireDays,LastCycled) AS ExpireDate FROM express.tblIntegrationKeys LEFT JOIN express.tblIntegrationMethods On tblIntegrationKeys.AccessAllowedTo = tblIntegrationMethods.MethodID WHERE tblIntegrationMethods.Status = 'Active' AND tblIntegrationKeys.Status = 'Active' AND DATEADD(day,ExpireDays,LastCycled) >= GETDATE() AND tblIntegrationKeys.CurrentKey = @strKey AND FunctionCall = @strMethod";

		_logger.LogInformation("Checking Function API Key Authorization");
		_logger.LogInformation("FunctionID: " + _functionID);
		_logger.LogInformation("APIKey: " + apiKey);

		var dataStore = DataStoreFactory.GetExpressDS();
		dataStore.AddParameter("@strMethod", _functionID);
		dataStore.AddParameter("@strKey", apiKey);
		var result = dataStore.RunGet(strQuery);

		// failure to connect to db
		if (result.IsT1)
		{
			return new ErrorMessage
			{
				Message = result.AsT1.Message,
				StackTrace = result.AsT1.StackTrace,
				StatusCode = HttpStatusCode.InternalServerError
			};
		}

		var dsFunctions = result.AsT0;

		// failure to find function
		if (dsFunctions.Tables[0].Rows.Count == 0)
		{
			_logger.LogInformation("No Function found.");

			return new ErrorMessage
			{
				Message = "You are not authorized to use this function.",
				StackTrace = "",
				StatusCode = HttpStatusCode.BadRequest
			};
		}

		_logger.LogInformation("APIKey Authorized");
		return true;
	}*/
}