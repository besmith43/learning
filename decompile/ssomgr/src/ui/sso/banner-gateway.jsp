<?xml version="1.0" encoding="ISO-8859-1" ?>
<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ taglib prefix="spring" uri="http://www.springframework.org/tags" %>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

	<jsp:include page="/ui/include/header.jsp"></jsp:include>
	<input type="hidden" id="page-id" value="banner-gateway" />
	<div id="content">
		
		<div id="leftpane">
			<span class="title">
				Banner SSO Gateway<hr />
			</span>
			<span>
				SSO Manager functions as an SSO gateway to the following applications:	 		
			</span>
			<ul>
				<li>Banner Self Service </li>
				<li>Internet Native Forms</li>
			</ul>
			<span class="title">
				SSO Service Provider <hr />
			</span>
			<span>
				SSO Manager exposes the following services that can be used by other applications 
				<br /> to facilitate claims-based SSO.
			</span>
			<ul>
				<li>Ticketing Service</li>
				<li>Credential Service</li>
			</ul>
			<span class="title">
				Gateway Configuration <hr />
			</span>
			<span>
				SSO Manager supports the following two SSO Protocols
			</span>
			<ul>
				<li>CAS (SAML 1.1 Ticket Validation)</li>
				<li>SAML 2.0</li>
			</ul>
		</div>
		
		<div id="rightpane">
			<div id="verificationlinks">
				<p class="title">
					Gated Applications<hr />
				</p>
				<ul>				
					<li>
					<c:choose>
						<c:when test="${ssb_verify_link == 'true'}">
							<a href="${pageContext.request.contextPath}${ssb_url}" 
							class="external" target="_ssb">Self Service Banner</a>
						</c:when>
						<c:otherwise>
							<span class="external">Self Service Banner</span>					
						</c:otherwise>
					</c:choose>
					</li>	
					<li>
					<c:choose>
						<c:when test="${inb_verify_link == 'true'}">
							<a href="${pageContext.request.contextPath}${inb_url}" 
							class="external" target="_inb">Internet Native Banner</a>
						</c:when>
						<c:otherwise>
							<span class="external">Internet Native Banner</span>					
						</c:otherwise>
					</c:choose>
					</li>	
				</ul>
			</div>
			<div id="ssoverificationlink">
				<p class="title">
					Verification Links  <hr />
				</p>
				<ul>
					<li>
					<c:choose>
						<c:when test="${mode == 'cas'}">
							<a href="${cas_url}" class="external" target="_cas">CAS</a></li>
						</c:when>
						<c:otherwise>
							<a href="${saml_url}" class="external" target="_saml">SAML</a></li>					
						</c:otherwise>
					</c:choose>	
					<li><a href="<%=request.getContextPath()%>/ws/credential-service.wsdl" class="external" target="_cws">Credential Service</a></li>
					<li><a href="<%=request.getContextPath()%>/ws/sso-ticket-service.wsdl" class="external" target="_tws">Ticketing Service</a></li>
				</ul>
		
			</div>
		</div>
		
	</div>
	
	<jsp:include page="/ui/include/footer.jsp"></jsp:include>
