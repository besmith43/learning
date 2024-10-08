<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<%@taglib uri="com.ellucian.ssomgr" prefix="sso" %>
<head>
<link rel="icon" href="<%=request.getContextPath()%>/ui/css/images/favicon.ico"/>
<title>SSO Manager - Login</title>
<link type="text/css" href="<%=request.getContextPath()%>/ui/css/vantagesplash.css" rel="stylesheet" />
</head>
<body>
<%response.addHeader( "X-FRAME-OPTIONS", "DENY" );%>
<input type="hidden" id="contextPath" value="<%=request.getContextPath()%>" />
<div>
 <div class="midBox">
  <div class="headTitle">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="70%" align="left"></td>
        <td width="30%" valign="bottom" class="slogan">SSO Manager</td>
      </tr>
    </table>
  </div>
  <div class="formContainer">
  <form method="post" action="j_spring_security_check">
    <table width="50%" border="0" align="right" cellpadding="0" cellspacing="0">
      <tr>
        <td>&nbsp;</td>
        <td><input type="text" class="username" border="0" name="j_username" maxlength="17" /></td>
        <td><input type="password"  class="password" name="j_password" maxlength="17"  /></td>
        <td><input type="submit" class="actionBut" value="Sign In"/></td>
      </tr>
      </table>
  </form>      
  </div>
  <div class="contentContainer">2023 Ellucian. All rights reserved.<br>
  <br />
    This software contains confidential and proprietary information of Ellucian and its subsidiaries. 
    Use of this software is limited to Ellucian licensees, and is subject to the terms and 
    conditions of one or more written license agreements between Ellucian and the licensee in 
    question.
    <br /><br />
    Build Version: ${sso:buildVersion()} &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Build Number: ${sso:buildNumber()} &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Build Date: ${sso:buildDate()}
   </div>
  </div>
 </div>
</body>
</html>