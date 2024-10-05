<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" 
"http://www.w3.org/TR/html4/loose.dtd">
<mytag id="myDiv">
<html>
<head>
<script type="text/javascript" src="<%=request.getContextPath()%>/ui/js/jquery-1.6.2.min.js"></script>
<script type="text/javascript" src="<%=request.getContextPath()%>/ui/js/sso-manager.js"></script>
<script>
	var req = null;
	function gotoSsb() {
		redirect("${pageContext.request.contextPath}/c/auth/SSB", "ssb");
	}
	function gotoInb() {
		redirect("${pageContext.request.contextPath}/c/auth/INB", "inb");
	}
	function redirect(url, app) {
		var paramName = document.frmTest.paramName.value;
		var paramValue = document.frmTest.paramValue.value;
		var typeOptions = document.frmTest.paramType;
		var paramType = typeOptions.options[typeOptions.selectedIndex].value;
		var methodOptions = document.frmTest.httpMethod;
		var httpMethod = methodOptions.options[methodOptions.selectedIndex].value;
		if("COOKIE" == paramType) {
			document.frmSubmit.action = "${pageContext.request.contextPath}/test/auth_mode_cookie.jsp";
			document.frmSubmit.method = "POST";
			addField("paramName", paramName);
			addField("paramValue", paramValue);
			addField("httpMethod", httpMethod);
			addField("url", url);
			document.frmSubmit.submit();
		}
		else if("HEADER" == paramType) {
			$("#msgDiv").html("Please wait while the request is being processed...");
			url += "?id=11235&name=Smith&City=Chicago";
			req = new XMLHttpRequest();
			req.open(httpMethod, url, false);
			req.setRequestHeader(paramName, paramValue);
			if(app == "inb") {
				req.onreadystatechange = function() {
	  				if (req.readyState==4) {
	  					if(req.status==200) {
	   						$("#myDiv").html(req.responseText);
	    				}
	  					else if(req.status==0) {
	   						$("#msgDiv").html
("The server has returned a SUCCESS response. Pls check the log file ssomgr.log for more information");
	    				}
	    				else {
	    					$("#msgDiv").html("Error processing request. Please try again.");
	    				}
	    			}
	    		};
	    	}
			if(app == "ssb") {
				req.onreadystatechange = function() {
	  				if (req.readyState==4) {
	  					if(req.status==0) {
	   						$("#msgDiv").html
("The server has returned a SUCCESS response. Pls check the log file ssomgr.log for more information");
	    				}
	    				else {
	    					$("#msgDiv").html("Error processing request. Please try again.");
	    				}
	    			}
	    		};
	    	}
			req.send();
		}
		else if("PARAMETER" == paramType) {
			document.frmSubmit.action = url;
			document.frmSubmit.method = httpMethod;
			addField(paramName, paramValue);
			addField("id", "11235");
			addField("name", "Smith");
			addField("city", "Chicago");
			document.frmSubmit.submit();			
		}
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
<body>
	<center>
	<br />
	<b>SSO Manager AUTH Mode Testing</b>
	<br />
	<form name="frmTest" method="POST">
		<br />
		<div align="center" id="msgDiv" style="color:red">&nbsp;</div>
		<table width="100%">
		<tr><td>Parameter Name:</td>
		<td align="left">
			<input type="input" name="paramName" value="udcid" />
		</td></tr>
		<tr><td>Parameter Value:</td>
		<td align="left">
			<input type="input" name="paramValue" value="6AF6C74CA08D33DDE040007F01006717" size="50" />
		</td></tr>
		<tr><td>Parameter Type:</td>
		<td  align="left"> 
			<select name="paramType">
				<option value="COOKIE">COOKIE</option>
				<option value="HEADER">HEADER</option>
				<option  value="PARAMETER">PARAMETER</option>
			</select>
		</td></tr>
		<tr><td>HTTP Method:</td>
		<td align="left"> 
			<select name="httpMethod">
				<option value="GET">GET</option>
				<option value="POST">POST</option>
			</select>
		</td></tr>
		<tr><td><br /></td></tr>
		<tr align="center"><td colspan="2"><input type="button" name="btnSsb" value="Invoke SSB Handler" 
		onclick="javascript:gotoSsb();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		<input type="button" name="btnInb" value="Invoke INB Handler" 
		onclick="javascript:gotoInb();" /></td></tr>
		</table>
	</form>
	<form name="frmSubmit">
	</form>
	</center>
</body>
</html>
</mytag>
