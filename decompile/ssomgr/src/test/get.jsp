<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" 
"http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<script>
	function gotoSsb() {
		document.frmSubmit.action = "${pageContext.request.contextPath}/c/SSB";
		addHiddenFields();
		document.frmSubmit.submit();
	}
	function gotoInb() {
		document.frmSubmit.action = "${pageContext.request.contextPath}/c/INB";
		addHiddenFields();
		document.frmSubmit.submit();
	}
	function addHiddenFields() {
		var param1 = document.frmTest.param1.value;
		var param2 = document.frmTest.param2.value;
		var param3 = document.frmTest.param3.value;
		var val1 = document.frmTest.val1.value;
		var val2 = document.frmTest.val2.value;
		var val3 = document.frmTest.val3.value;
		addField(param1, val1);
		addField(param2, val2);
		addField(param3, val3);
		//var qry = "?" + param1 + "=" + val1 + "&" + param2 + "=" + val2 + "&" + param3 + "=" + val3;
		//return qry;
	}
	function addField(param, val) {
		var hiddenField = document.createElement("input");             
		hiddenField.setAttribute("type", "hidden");             
		hiddenField.setAttribute("name", param);             
		hiddenField.setAttribute("value", val);              
		frmSubmit.appendChild(hiddenField);
	}
</script>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>SSO Manager - HTTP GET Testing - CAS Mode</title>
</head>
<body>
	<center>
	<br />
	<b>SSO Manager HTTP GET Testing for CAS Mode</b>
	<br /><br />
	<form name="frmTest" method="GET">
		<table>
			<tr>
				<td>Parameter Name:</td><td><input type="input" name="param1" value="id" /></td>
				<td>&nbsp;&nbsp;&nbsp;Parameter Value:</td><td><input type="input" name="val1" value="11235" /></td>
			</tr>
			<tr><td><br /></td></tr>
			<tr>
				<td>Parameter Name:</td><td><input type="input" name="param2" value="name" /></td>
				<td>&nbsp;&nbsp;&nbsp;Parameter Value:</td><td><input type="input" name="val2" value="Smith" /></td>
			</tr>
			<tr><td><br /></td></tr>
			<tr>
				<td>Parameter Name:</td><td><input type="input" name="param3" value="city" /></td>
				<td>&nbsp;&nbsp;&nbsp;Parameter Value:</td><td><input type="input" name="val3" value="Chicago" /></td>
			</tr>
			<tr><td><br /></td></tr>
			<tr align="center">
				<td colspan="4">
					<input type="button" name="btnSsb" value="Invoke SSB Handler" 
					onclick="javascript:gotoSsb();" />
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<input type="button" name="btnInb" value="Invoke INB Handler" 
					onclick="javascript:gotoInb();" />
				</td>
			</tr>
		</table>
		<br /> 
	</form>
	<form name="frmSubmit">
	</form>
	</center>
</body>
</html>
