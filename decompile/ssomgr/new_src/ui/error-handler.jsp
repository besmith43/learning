<?xml version="1.0" encoding="ISO-8859-1" ?>
<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1" isErrorPage="true" %>
<%@ taglib prefix="spring" uri="http://www.springframework.org/tags" %>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
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
				<p class="title">
					<spring:message code="error.caption" /><hr />
				</p>
				<div>
					<table class="appConfigTbl" border="0">
						<tr><td class="alertText">${ssoexception}</td></tr>
					</table>
				</div>
			</div> <!-- End PageContent -->
	  	</div> <!-- End Content -->
	</body>	
</html>
	