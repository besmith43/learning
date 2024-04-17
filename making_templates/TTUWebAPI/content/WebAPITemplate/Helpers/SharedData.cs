using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITemplate.Helpers;

public static class SharedData
{
#if DEBUG
    public static bool blnDebug = true;
#else
    public static bool blnDebug = false;
#endif

    public static string ExpressDBConnectionString = "Data Source=SQLCLS03;Initial Catalog = PortalProd; Integrated Security=True;TrustServerCertificate=True;";
    public static string ServicesSafariDBConnectionString = @"Data Source=SQLCLS03;Initial Catalog=ServicesSafariProd; Integrated Security=True;TrustServerCertificate=True;";
    public static string Instance = "Production";
    public static string strDevEmails = String.Empty;
}