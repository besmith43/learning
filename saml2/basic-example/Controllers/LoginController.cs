using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using api.Models;
using api.Utils;
using api.Authorization;
using api.Authentication;
// using DataStore.Model;
using Microsoft.AspNetCore.Http.Extensions;
// using DataStore;
using System.Text;
using api.Models.SAML;

namespace api.Controllers;

[ApiController]
// [ServiceFilter(typeof(AuthenticationAttribute))]
// [Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly IConfiguration _config;

    public LoginController(ILogger<LoginController> logger, IConfiguration config)
    {
        _config = config;
        _logger = logger;
    }

	[Route("api/validateCredentials")]
	[HttpGet]
	[ServiceFilter(typeof(AuthenticationAttribute))]
    public IResult validateCredentials()
    {
		/*
			This endpoint basically does nothing,
			because all the logic that is needed already exists in the Authentication Attribute.
			Therefore as long as the call to this endpoint returns a 200 (Ok) status, you're good!
		*/
		return Results.Ok("Success");
    }

	[Route("api/samlRequestURL")]
	[HttpGet]
	public IResult getSamlRequestURL()
    {
        var samlInfo = SharedData.SamlInfo;

        // Generates the request for SAML authentication and provides the URL with the associated request information
        var request = new AuthRequest(samlInfo.SAMLApplicationID, samlInfo.SAMLConsumerURL);
        var url = request.GetRedirectUrl(samlInfo.SAMLEndPoint);
        return Results.Ok(url.ToString());
    }

	[Route("api/verifySamlLogin")]
	[HttpPost]
	// public IResult verifySamlLogin(HttpContext context, string SAMLResponse, string strIPAddress)
	public IResult verifySamlLogin(HttpContext context)
    {
        // Provides a method for the presentation layer to verify a user has successfully logged into the SAML Provider
        // Literal string that contains the Certificate of the SAML ServiceProvider Endpoint

		var sessionID = context.Request.Cookies["sharkCookie"];
		var SAMLResponse = context.Request.Form["SAMLResponse"];

        if (String.IsNullOrEmpty(sessionID))
        {
            sessionID = Guid.NewGuid().ToString();
        }

        var samlInfo = SharedData.SamlInfo;

        string samlCertificate = samlInfo.SAMLCert;
        // string strSAMLResponse = SAMLResponse.Substring(0, SAMLResponse.IndexOf("EndOfSAML"));
        // string strSessionInfo = SAMLResponse.Substring(SAMLResponse.IndexOf("EndOfSAML") + 9);

        SamlResponse samlResponse = new SamlResponse(samlCertificate);
        samlResponse.LoadXmlFromBase64(SAMLResponse);
        //If the samlResponse is valid then the function will get the provided data passed by the SAML provider and attempts to instert it as a new session 
        if (samlResponse.IsValid())
        {
            // string username, email;//, firstname, lastname, groups, udcid, dn, strBannerUserName;
            string username;
            List<string> lstGroups = new List<string>();

            try
            {
                username = samlResponse.GetNameID();
                // email = samlResponse.GetEmail();
                // firstname = samlResponse.GetFirstName();
                // lastname = samlResponse.GetLastName();
                // udcid = "NA";
                // dn = "NA";
                // dn = samlResponse.GetDN();
                lstGroups.Add("Staff");
                lstGroups.Add("All Users");
                lstGroups.Add("LifeTime Password");
                
                // groups = "";
                // foreach (string strCurrent in lstGroups)
                // {
                    // groups += strCurrent + ",";
                // }
                // groups = groups.Remove(groups.Length - 1, 1);

                // udcid = samlResponse.GetUDCID();

				// this is going to have to talk to Banner for the UDCID unless we can get it from AD
                // strBannerUserName = findUsernameWithUDCID(udcid);
            }
            catch (Exception ex)
            {
                return Results.Problem("ERROR: " + ex.ToString());
            }
            /*if (strBannerUserName == "Error" || (strBannerUserName.Trim().ToUpper() == username.Trim().ToUpper()))
            {
                return JsonConvert.SerializeObject(addNewSession(username, DateTime.Now, DateTime.Now, groups, strSessionInfo, udcid, strIPAddress, username));
            }
            return JsonConvert.SerializeObject(addNewSession(findUsernameWithUDCID(udcid), DateTime.Now, DateTime.Now, groups, strSessionInfo, udcid, strIPAddress, username));*/

            var currentSession = new CurrentSession();
            currentSession.SessionID = sessionID;
            currentSession.ADUserID = username;
            currentSession.UserID = username;
            currentSession.ADGroups = lstGroups;
            currentSession.SessionStartDateTime = DateTime.Now;

            SharedData.lstCurrentSessions.Add(currentSession);

			return Results.Ok(currentSession);
        }
        else
        {
            return Results.BadRequest("ERROR - NOT VALID SAML");
        }
    }

/*
// this is taken from Express's Application layer and is used as a basis for reproducing SSO authentication for the Computer Inventory Project


// first thing that gets called if not signed in
    [WebMethod]
    public string validateCredentials(string strSessionID)
    {
        //WebMethod utilizes the PHP Session ID generated/disposed by PHP to verify if a session is valid.
        addServiceLogEntry("validateCredentials", "strSessionID: " + strSessionID);
        return JsonConvert.SerializeObject(TTU.getSessionDetails(strSessionID));
    }

// second thing that's called if not signed in
	[WebMethod]
    public string getSamlRequestURL()
    {
        // Generates the request for SAML authentication and provides the URL with the associated request information
        addServiceLogEntry("getSamlRequestURL", "Called");
        var request = new AuthRequest(getLocalizationValue("SAMLApplicationID"), getLocalizationValue("SAMLConsumerURL"));
        var url = request.GetRedirectUrl(getLocalizationValue("SamlEndPoint"));
        return url.ToString();
    }

// actually handles most of the SAML stuffs
    [WebMethod]
    public string verifySamlLogin(string SAMLResponse, string strIPAddress)
    {
        // Provides a method for the presentation layer to verify a user has successfully logged into the SAML Provider
        // Literal string that contains the Certificate of the SAML ServiceProvider Endpoint

        string samlCertificate = getLocalizationValue("SAMLCert");
        string strSAMLResponse = SAMLResponse.Substring(0, SAMLResponse.IndexOf("EndOfSAML"));
        string strSessionInfo = SAMLResponse.Substring(SAMLResponse.IndexOf("EndOfSAML") + 9);

        SamlResponse samlResponse = new SamlResponse(samlCertificate);
        samlResponse.LoadXmlFromBase64(strSAMLResponse);
        //If the samlResponse is valid then the function will get the provided data passed by the SAML provider and attempts to instert it as a new session 
        if (samlResponse.IsValid())
        {

            string username, email, firstname, lastname, groups, udcid, dn, strBannerUserName;
            try
            {
                username = samlResponse.GetNameID();
                email = samlResponse.GetEmail();
                firstname = samlResponse.GetFirstName();
                lastname = samlResponse.GetLastName();
                udcid = "NA";
                dn = "NA";
                dn = samlResponse.GetDN();
                List<string> lstGroups = GetNestedGroupMembershipsByTokenGroup(dn);
                groups = "";
                foreach (string strCurrent in lstGroups)
                {
                    groups += strCurrent + ",";
                }
                groups = groups.Remove(groups.Length - 1, 1);
                udcid = samlResponse.GetUDCID();
                strBannerUserName = findUsernameWithUDCID(udcid);
            }
            catch (Exception ex)
            {
                addErrorLogEntry("verifySAMLLogin", ex.ToString());
                return "ERROR: " + ex.ToString();
            }
            if (strBannerUserName == "Error" || (strBannerUserName.Trim().ToUpper() == username.Trim().ToUpper()))
            {
                return JsonConvert.SerializeObject(addNewSession(username, DateTime.Now, DateTime.Now, groups, strSessionInfo, udcid, strIPAddress, username));
            }
            return JsonConvert.SerializeObject(addNewSession(findUsernameWithUDCID(udcid), DateTime.Now, DateTime.Now, groups, strSessionInfo, udcid, strIPAddress, username));
        }
        else
        {
            return "ERROR - NOT VALID SAML";
        }
    }
*/
}