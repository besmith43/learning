<?xml version="1.0" encoding="ISO-8859-1" ?>
<%@ page language="java" contentType="text/html; charset=ISO-8859-1" pageEncoding="ISO-8859-1"%>
<%@ taglib prefix="spring" uri="http://www.springframework.org/tags" %>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/functions" prefix="fn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

	<head>
	<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
	<title>${title}</title>
	<link type="text/css" href="<%=request.getContextPath()%>/ui/css/common-controls.css" rel="stylesheet" />
	<link type="text/css" href="<%=request.getContextPath()%>/ui/css/common-platform.css" rel="stylesheet" />
	<link type="text/css" href="<%=request.getContextPath()%>/ui/css/menu.css" rel="stylesheet" />
	<link type="text/css" href="<%=request.getContextPath()%>/ui/css/sso-manager.css" rel="stylesheet" />
	<script type="text/javascript" src="<%=request.getContextPath()%>/ui/js/jquery-1.6.2.min.js"></script>
	<script type="text/javascript" src="<%=request.getContextPath()%>/ui/js/sso-manager.js"></script>
	</head>
	<body>
	
	<input type="hidden" id="contextPath" value="<%=request.getContextPath()%>" />
	
	<div id="content">
	
		<div id="pagecontent">
			<span class="title"><spring:message code="oracle.pwd.prompt" /><hr /></span>
			
			<div id="pwdPromptDiv">
				<form:form modelAttribute="oraclePwdPromptConfig" id="frmPwdPrmpt">
				<input type="hidden" id="page-id" value="oracle-pwd-prompt" />
				<form:hidden path="ssoMgrOracleUserId" />
				<form:hidden path="ssoMgrOraclePwdValidateUrl" />
				<form:hidden path="ssoMgrInbHandlerUrl" />
				<center>
				<table id="pwdPromptTbl">
					<tr>
						<td colspan="2" class="alertText" id="tdMsg">&nbsp;</td>
					</tr>
					<tr id="trCntr">
						<td colspan="2">
							<span class="title">Oracle Password for <c:out value="${oraclePwdPromptConfig.ssoMgrOracleUserId}" /></span>
						</td>
					</tr>
					<tr id="trCntr">
						<td colspan="2"><form:password path="ssoMgrOraclePwd" maxlength="30" /></td>
					</tr>
					<tr id="trCntr">
						<td colspan="2">
							<img src="<%=request.getContextPath()%>/ui/css/images/save.png" border="0"
							id="btnSvOraclePwd" />&nbsp;
						</td>
					</tr>
				</table>
				</center>
				</form:form>
			</div>	
			
		</div> <!-- End PageContent -->
	
	</div> <!-- End Content -->
	
	</body>
</html>