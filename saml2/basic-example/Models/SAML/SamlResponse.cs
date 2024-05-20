using System.Xml;
using System.Security.Cryptography.Xml;

namespace api.Models.SAML;

public class SamlResponse
{
	private XmlDocument _xmlDoc;
	private Certificate _certificate;
	private XmlNamespaceManager _xmlNameSpaceManager; //we need this one to run our XPath queries on the SAML XML

	public string Xml { get { return _xmlDoc.OuterXml; } }

	public SamlResponse(string certificateStr)
	{
		// RSAPKCS1SHA256SignatureDescription.Init(); //init the SHA256 crypto provider (for needed for .NET 4.0 and lower)

		_certificate = new Certificate();
		_certificate.LoadCertificate(certificateStr);
	}

	public void LoadXml(string xml)
	{
		_xmlDoc = new XmlDocument();
		_xmlDoc.PreserveWhitespace = true;
		_xmlDoc.XmlResolver = null;
		_xmlDoc.LoadXml(xml);

		_xmlNameSpaceManager = GetNamespaceManager(); //lets construct a "manager" for XPath queries
	}

	public void LoadXmlFromBase64(string response)
	{
		System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
		LoadXml(enc.GetString(Convert.FromBase64String(response)));
	}

	public bool IsValid()
	{
		XmlNodeList nodeList = _xmlDoc.SelectNodes("//ds:Signature", _xmlNameSpaceManager);

		SignedXml signedXml = new SignedXml(_xmlDoc);

		if (nodeList.Count == 0) return false;

		signedXml.LoadXml((XmlElement)nodeList[0]);
		return ValidateSignatureReference(signedXml) && signedXml.CheckSignature(_certificate.cert, true) && !IsExpired();
	}

	//an XML signature can "cover" not the whole document, but only a part of itf
	//.NET's built in "CheckSignature" does not cover this case, it will validate to true.
	//We should check the signature reference, so it "references" the id of the root document element! If not - it's a hack
	private bool ValidateSignatureReference(SignedXml signedXml)
	{
		if (signedXml.SignedInfo.References.Count != 1) //no ref at all
			return false;

		var reference = (Reference)signedXml.SignedInfo.References[0];
		var id = reference.Uri.Substring(1);

		var idElement = signedXml.GetIdElement(_xmlDoc, id);

		if (idElement == _xmlDoc.DocumentElement)
			return true;
		else //sometimes its not the "root" doc-element that is being signed, but the "assertion" element
		{
			var assertionNode = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion", _xmlNameSpaceManager) as XmlElement;
			if (assertionNode != idElement)
				return false;
		}

		return true;
	}

	private bool IsExpired()
	{
		DateTime expirationDate = DateTime.MaxValue;
		XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:Subject/saml:SubjectConfirmation/saml:SubjectConfirmationData", _xmlNameSpaceManager);
		if (node != null && node.Attributes["NotOnOrAfter"] != null)
		{
			DateTime.TryParse(node.Attributes["NotOnOrAfter"].Value, out expirationDate);
		}
		return DateTime.UtcNow > expirationDate.ToUniversalTime();
	}

	public string GetNameID()
	{
		XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:Subject/saml:NameID", _xmlNameSpaceManager);
		return node.InnerText;
	}

	public string GetXMLSaml()
	{

		StringWriter sw = new StringWriter();
		XmlTextWriter xw = new XmlTextWriter(sw);
		_xmlDoc.WriteTo(xw);
		String XmlString = sw.ToString();
		return XmlString;
	}

	public string GetUDCID()
	{
		XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='udcid']/saml:AttributeValue", _xmlNameSpaceManager);
		return node == null ? null : node.InnerText;
	}

	public string GetDN()
	{
		XmlNodeList node = _xmlDoc.SelectNodes("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='DN']/saml:AttributeValue", _xmlNameSpaceManager);
		string strDN = "";
		for (int i = 0; i < node.Count; i++)
		{
			if (i == 0)
			{
				strDN = node[i].InnerText;
			}
			else
			{
				strDN = strDN + "," + node[i].InnerText;
			}

		}
		return strDN;
	}

	public string GetEmail()
	{
		XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='User.email']/saml:AttributeValue", _xmlNameSpaceManager);

		//some providers (for example Azure AD) put email into an attribute named "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
		if (node == null)
			node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']/saml:AttributeValue", _xmlNameSpaceManager);

		return node == null ? null : node.InnerText;
	}

	public string GetFirstName()
	{
		XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='first_name']/saml:AttributeValue", _xmlNameSpaceManager);

		//some providers (for example Azure AD) put email into an attribute named "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"
		if (node == null)
			node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname']/saml:AttributeValue", _xmlNameSpaceManager);

		return node == null ? null : node.InnerText;
	}

	public string GetLastName()
	{
		XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='last_name']/saml:AttributeValue", _xmlNameSpaceManager);

		//some providers (for example Azure AD) put email into an attribute named "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"
		if (node == null)
			node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname']/saml:AttributeValue", _xmlNameSpaceManager);
		return node == null ? null : node.InnerText;
	}

	public string GetDepartment()
	{
		XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/department']/saml:AttributeValue", _xmlNameSpaceManager);
		return node == null ? null : node.InnerText;
	}

	public string GetPhone()
	{
		XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone']/saml:AttributeValue", _xmlNameSpaceManager);
		if (node == null)
			node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/telephonenumber']/saml:AttributeValue", _xmlNameSpaceManager);
		return node == null ? null : node.InnerText;
	}

	public string GetCompany()
	{
		XmlNode node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/companyname']/saml:AttributeValue", _xmlNameSpaceManager);
		return node == null ? null : node.InnerText;
	}

	public string GetGroups()
	{
		XmlNodeList node = _xmlDoc.SelectNodes("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='Groups']/saml:AttributeValue", _xmlNameSpaceManager);
		string strGroups = "";
		for (int i = 0; i < node.Count; i++)
		{
			if (i == 0)
			{
				strGroups = node[i].InnerText;
			}
			else
			{
				strGroups = strGroups + "," + node[i].InnerText;
			}

		}
		return strGroups;
		//return node == null ? null : node.InnerText;
	}

	//returns namespace manager, we need one b/c MS says so... Otherwise XPath doesnt work in an XML doc with namespaces
	//see https://stackoverflow.com/questions/7178111/why-is-xmlnamespacemanager-necessary
	private XmlNamespaceManager GetNamespaceManager()
	{
		XmlNamespaceManager manager = new XmlNamespaceManager(_xmlDoc.NameTable);
		manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
		manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
		manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

		return manager;
	}
}