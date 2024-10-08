<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<%@taglib uri="com.ellucian.ssomgr" prefix="sso" %>
<head>
<title>SAML Logout</title>
<link type="text/css" href="<%=request.getContextPath()%>/ui/css/vantagesplash.css" rel="stylesheet" />
</head>
<body>
<input type="hidden" id="contextPath" value="<%=request.getContextPath()%>" />
<div>
 <div class="midBox">
  <div class="formContainer">
  <form method="post" action="j_security_check">
    <table width="50%" border="0" align="left" cellpadding="0" cellspacing="0">
      <tr>
       <td class="slogan"><FONT SIZE=4>SAML Logout Successful</FONT></td>	
      </tr>
      <tr>
       <td><a href="<%=request.getContextPath()%>/saml/login?relayState=/c/auth/SSB" ><font color="ffffff">Self Service Banner</font></a></td>	
      </tr>
      </table>
  </form>      
  </div>
  </div>
 </div>
</body>
</html>
