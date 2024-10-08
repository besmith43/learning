<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ taglib prefix="spring" uri="http://www.springframework.org/tags" %>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

	<jsp:include page="/ui/include/header.jsp"></jsp:include>
	<input type="hidden" id="page-id" value="inb" />
	<div id="content">
	
		<div id="pagecontent" class="title">
			<spring:message code="inb.caption" /><hr />
			
			<div id="inbconfigtable">
				<form:form modelAttribute="inbConfig" id="frmInb">
				<form:hidden path="id" />
				<table>
					<tr>
						<td class="alertText" colspan="2">
							<c:if test="${'true' == inb_success_msg}">
								<span><spring:message code="inb.save-success-msg" /></span>
							</c:if>								
						</td>
					</tr>
					<tr>
						<td class="text"><spring:message code="inb.url" /></td>
						<td class="contentLeft">
							<form:input path="url" class="txtFld" maxlength="500" size="80" />
							<span class="alertText"><form:errors path="url" /></span>
						</td>
					</tr>
					<tr>
						<td class="text"><spring:message code="inb.database" /></td>
						<td class="contentLeft">
							<form:input path="database"  class="txtFld" size="40" maxlength="50" />
							<span class="alertText"><form:errors path="database" /></span>
						</td>
					</tr>
					<tr rowspan="4">
						<td class="text" valign="top"><spring:message code="inb.mode" /></td>
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
						        <tr class="appConfigTblAuthRow">
                                    <td>
                                        <spring:message code="inb.udcid-indicator" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="contentLeft">
                                        <form:select path="udcIdIndicator" multiple="false" class="txtFld">
                                            <form:options items="${udcIdIndicators}" />
                                        </form:select>
                                    </td>
                                    <td>
                                        <span class="alertText"><form:errors path="udcIdIndicator" /></span>
                                    </td>
						        </tr>
						        <tr class="appConfigTblAuthRow">
                                    <td><span><spring:message code="inb.udcid-key" /></span></td>
                                    <td class="contentLeft">
                                        <form:input path="udcIdKey" class="txtFld" size="20" maxlength="100" />
                                    </td>
                                    <td>
                                        <span class="alertText"><form:errors path="udcIdKey" /></span>
                                    </td>
                                </tr>
                            </table>
						</td>
                    </tr>
					<tr>
						<td class="text"><spring:message code="inb.ticket-param-name" /></td>										
						<td class="contentLeft">
							<form:input path="ticketParameterName"  class="txtFld" size="40" maxlength="50" />
							<span class="alertText"><form:errors path="ticketParameterName" /></span>
						</td>
					</tr>
				</table>
				
				<div id="pwdPolicy">
					<p><span class="title"><spring:message code="inb.pwd.policy" /><hr /></span>
					<table>
						<tr>
							<td colspan="2" id="tdMsg" class="alertText">&nbsp;<form:errors path="passwordPolicy" /></td>
						</tr>
						<tr>
							<td colspan="2" class="contentLeft">
								<form:radiobutton path="passwordPolicy" value="P" 
								id="prmptPolicy" label="Prompt" />&nbsp;
								<form:radiobutton path="passwordPolicy" value="G" 
								id="genPolicy" label="Auto Generate" />&nbsp;
							</td>
						</tr>		
						<tr>
							<td class="text">
								<span ><spring:message code="inb.pwd.valid.chars" /></span>
							</td>
							<td class="contentLeft">
								<form:select path="pwdValidChars" class="txtFld">
									<form:options items="${pwdValidCharSet}" />
								</form:select>
							</td>
						</tr>
						<tr>
							<td class="text">
								<span><spring:message code="inb.pwd.min.length" /></span>
							</td>
							<td class="contentLeft">
								<form:select path="pwdMinLength" class="txtFld" multiple="false">
									<form:options items="${pwdLength}" />
								</form:select>
								<span class="alertText"><form:errors path="pwdMinLength" /></span>
							</td>
						</tr>
						<tr>
							<td class="text">
								<span ><spring:message code="inb.pwd.max.length" /></span>
							</td>
							<td class="contentLeft">
								<form:select path="pwdMaxLength" class="txtFld" multiple="false">
									<form:options items="${pwdLength}" />
								</form:select>
							</td>
						</tr>
						<tr>
							<td colspan="2" class="contentLeft">
							<form:checkbox path="storePassword" />&nbsp;
							<span><spring:message code="inb.pwd.store" /></span></td>
						</tr>
					</table></p>
				</div><!-- Div End Passord Policy-->
				</form:form>
			</div>	<!-- End INB Config Div-->
			
			<div id="buttonbar">	
				<img src="<%=request.getContextPath()%>/ui/css/images/purge_credentials.jpg" border="0" id="btnFlshInb" />
				<img src="<%=request.getContextPath()%>/ui/css/images/save.png" border="0" id="btnSaveInb" />
				<img src="<%=request.getContextPath()%>/ui/css/images/cancel.png" border="0" id="btnCnclInb" />
			</div> <!-- End of Button Bar -->
			
		</div> <!-- End PageContent -->
	
	</div> <!-- End Content -->
	
	<jsp:include page="/ui/include/footer.jsp"></jsp:include>
