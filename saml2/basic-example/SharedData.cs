using api.Models;
using api.Models.SAML;

namespace api;

public static class SharedData
{
#if DEBUG
    public static bool blnDebug = true;
#else
    public static bool blnDebug = false;
#endif

	public static List<CurrentSession> lstCurrentSessions = new List<CurrentSession>();
	public static List<ApiKey> lstApiKeys = new List<ApiKey>();
	public static SAMLInfo SamlInfo = new SAMLInfo();
}