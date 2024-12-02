using System.Xml;
using System.IO.Compression;
using System.Text;
using System.Web;

namespace api.Models.SAML;


public class AuthRequest
{
    public string _id;
    private string _issue_instant;

    private string _issuer;
    private string _assertionConsumerServiceUrl;

    public enum AuthRequestFormat
    {
        Base64 = 1
    }

    public AuthRequest(string issuer, string assertionConsumerServiceUrl)
    {
        // I'm hoping that this isn't needed anymore
        // RSAPKCS1SHA256SignatureDescription.Init(); //init the SHA256 crypto provider (for needed for .NET 4.0 and lower)

        _id = "_" + System.Guid.NewGuid().ToString();
        _issue_instant = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");

        _issuer = issuer;
        _assertionConsumerServiceUrl = assertionConsumerServiceUrl;
    }

    public string GetRequest(AuthRequestFormat format)
    {
        using (StringWriter sw = new StringWriter())
        {
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;

            using (XmlWriter xw = XmlWriter.Create(sw, xws))
            {
                xw.WriteStartElement("samlp", "AuthnRequest", "urn:oasis:names:tc:SAML:2.0:protocol");
                xw.WriteAttributeString("ID", _id);
                xw.WriteAttributeString("Version", "2.0");
                xw.WriteAttributeString("IssueInstant", _issue_instant);
                xw.WriteAttributeString("ProtocolBinding", "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST");
                xw.WriteAttributeString("AssertionConsumerServiceURL", _assertionConsumerServiceUrl);

                xw.WriteStartElement("saml", "Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
                xw.WriteString(_issuer);
                xw.WriteEndElement();

                xw.WriteStartElement("samlp", "NameIDPolicy", "urn:oasis:names:tc:SAML:2.0:protocol");
                xw.WriteAttributeString("Format", "urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified");
                xw.WriteAttributeString("AllowCreate", "true");
                xw.WriteEndElement();

                /*xw.WriteStartElement("samlp", "RequestedAuthnContext", "urn:oasis:names:tc:SAML:2.0:protocol");
                xw.WriteAttributeString("Comparison", "exact");
                xw.WriteStartElement("saml", "AuthnContextClassRef", "urn:oasis:names:tc:SAML:2.0:assertion");
                xw.WriteString("urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport");
                xw.WriteEndElement();
                xw.WriteEndElement();*/

                xw.WriteEndElement();
            }

            if (format == AuthRequestFormat.Base64)
            {
                //byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(sw.ToString());
                //return System.Convert.ToBase64String(toEncodeAsBytes);

                //https://stackoverflow.com/questions/25120025/acs75005-the-request-is-not-a-valid-saml2-protocol-message-is-showing-always%3C/a%3E
                var memoryStream = new MemoryStream();
                var writer = new StreamWriter(new DeflateStream(memoryStream, CompressionMode.Compress, true), new UTF8Encoding(false));
                writer.Write(sw.ToString());
                writer.Close();
                string result = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, Base64FormattingOptions.None);
                return result;
            }

            return null;
        }
    }

    //returns the URL you should redirect your users to (i.e. your SAML-provider login URL with the Base64-ed request in the querystring
    public string GetRedirectUrl(string samlEndpoint)
    {
        var queryStringSeparator = samlEndpoint.Contains("?") ? "&" : "?";

        return samlEndpoint + queryStringSeparator + "SAMLRequest=" + HttpUtility.UrlEncode(this.GetRequest(AuthRequest.AuthRequestFormat.Base64));
    }
}