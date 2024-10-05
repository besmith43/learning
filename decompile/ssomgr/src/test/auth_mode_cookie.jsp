<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<% 
	String url = request.getParameter("url");
	System.out.println(url);
	String httpMethod = request.getParameter("httpMethod");
	System.out.println(httpMethod);
	String udcIdKey = request.getParameter("paramName");
	System.out.println(udcIdKey);
	String udcIdVal = request.getParameter("paramValue");
	System.out.println(udcIdVal);
	Cookie cookie = new Cookie(udcIdKey, udcIdVal);
	cookie.setPath("/");
	response.addCookie(cookie);
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" 
"http://www.w3.org/TR/html4/loose.dtd">
<mytag id="myDiv">
<html>
<head>
<script type="text/javascript" src="<%=request.getContextPath()%>/ui/js/jquery-1.6.2.min.js"></script>
<script type="text/javascript" src="<%=request.getContextPath()%>/ui/js/sso-manager.js"></script>
<script>
	function submitForm() {
		document.frmSubmit.action = "${param["url"]}";
		document.frmSubmit.method = "${param["httpMethod"]}";
		addField("id", "11235");
		addField("name", "Smith");
		addField("city", "Chicago");
		document.frmSubmit.submit();
	}
	function addField(param, val) {
		var hiddenField = document.createElement("input");             
		hiddenField.setAttribute("type", "hidden");             
		hiddenField.setAttribute("name", param);             
		hiddenField.setAttribute("value", val);              
		document.frmSubmit.appendChild(hiddenField);
	}
</script>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>SSO Manager AUTH Mode testing</title>
</head>
<body onload="javascript:submitForm();">
	<center>
	<br />
	<b>SSO Manager AUTH Mode Testing</b>
	<br />
	<form name="frmSubmit">
	</form>
	</center>
</body>
</html>
</mytag>
