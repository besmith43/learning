<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ taglib prefix="spring" uri="http://www.springframework.org/tags" %>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
	<jsp:include page="/ui/include/header.jsp"></jsp:include>
	<input type="hidden" id="page-id" value="ssb" />
	<div id="content">
		<div id="pagecontent" class="title">				
			<spring:message code="ssb.caption" /><hr/>
			<div id="ssbconfig">
				<form:form modelAttribute="ssbConfig" id="frmSsb">
				<form:hidden path="id" />
				<table class="appConfigTbl">
					<c:if test="${'true' == ssb_success_msg}">
						<tr>
							<td class="alertText" colspan="2"><span >
							<spring:message code="ssb.save-success-msg" /></span></td>
						</tr>
					</c:if>								
					<tr>
						<td class="text"><spring:message code="ssb.url" /></td>
						<td class="contentLeft">
							<form:input path="url" class="txtFld" maxlength="500" size="80" />
							<span class="alertText"><form:errors path="url" /></span>					
						</td>
					</tr>
					<tr>
						<td class="text"></td>
						<td><spring:message code="ssb.deeplink.title" /> <form:checkbox path="deepLinking" id="dlink" /> <span class="noteText"><spring:message code="ssb.deeplink.note" /></span></td>
						<td></td>
					</tr>
                    <tr rowspan="4">
                        <td></td>
                        <td class="text">
                            <table>
                                <tr>
                                    <td>
                                        <spring:message code="ssb.base-url" /></td>
                                    <td><form:input path="baseUrl" class="uportal-text-small" maxlength="500" size="40" /></td>
                                    <td>
                                        <span class="alertText"><form:errors path="baseUrl" /></span>
                                    </td>
                                 </tr>
                                 <tr>
                                     <td class="text"><spring:message code="ssb.url-param-name" /> </td>
                                     <td><form:input path="urlParameterName" class="txtFld" size="40" maxlength="50" /></td>
                                     <td>
                                         <span class="alertText"><form:errors path="urlParameterName" /></span>
                                     </td>
                                </tr>
                         </table>
                        </td>
                    </tr>
					<tr rowspan="4">
					    <td class="text" valign="top"><spring:message code="ssb.mode" /></td>
                        <td class="contentLeft" colspan="2">
                            <form:radiobutton path="mode" value="SAML2.0" id="SamlMode" label="SAML2.0" />
                            &nbsp;
                            <form:radiobutton path="mode" value="CAS" id="casMode" label="CAS" />
                            &nbsp;
                            <form:radiobutton path="mode" value="AUTH" id="authMode" label="Third Party Access Manager" />
                            <span class="alertText"><form:errors path="mode" /></span>
                        </td>
					</tr>
					<tr>
                        <td></td>
                        <td>
                            <table>
                                <tr>
                                    <td><spring:message code="ssb.udcid-indicator" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                    <td>
                                        <form:select path="udcIdIndicator"	class="txtFld" multiple="false" >
                                            <form:options items="${udcIdIndicators}" />
                                        </form:select> <span class="alertText"><form:errors path="udcIdIndicator" /></span>
                                    </td>
                                    <td>
                                        <span class="alertText"><form:errors path="udcIdIndicator" /></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text"><spring:message code="ssb.udcid-key" /></td>
                                    <td class="contentLeft">
                                        <form:input path="udcIdKey" class="txtFld" size="16" maxlength="100" />
                                    </td>
                                    <td>
                                        <span class="alertText"><form:errors path="udcIdKey" /></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
					<tr>
						<td class="text"><spring:message code="ssb.cookie-name" /></td>
						<td class="contentLeft">
						    <form:input path="cookieName" class="txtFld" size="30" maxlength="50"/>
						    <span class="alertText"><form:errors path="cookieName" /></span>
						</td>
					</tr>
					<tr>
						<td class="text"><spring:message code="ssb.cookie-domain-name" /></td>										
						<td class="contentLeft">
							<form:input path="cookieDomainName" class="txtFld" size="30" maxlength="100" />
							<span class="alertText"><form:errors path="cookieDomainName" /></span>
						</td>
					</tr>
					<tr>
                        <td class="text"><spring:message code="ssb.webtailor.schema.name" /></td>
                        <td class="contentLeft">
                            <form:input path="webTailorSchemaName" class="txtFld" size="30" maxlength="30" />
                            <span class="alertText"><form:errors path="webTailorSchemaName" /></span>
                        </td>
                    </tr>
				</table> <!-- End of Button Bar -->
				</form:form>
			</div>			
			
			<div id="buttonbar">
				<img src="<%=request.getContextPath()%>/ui/css/images/save.png" border="0" id="btnSaveSsb"/>
				<img src="<%=request.getContextPath()%>/ui/css/images/cancel.png" border="0" id="btnCnclSsb" />
			</div>
						
		</div> <!-- End PageContent -->
		
  	</div> <!-- End Content -->
  	
	<jsp:include page="/ui/include/footer.jsp"></jsp:include>
		