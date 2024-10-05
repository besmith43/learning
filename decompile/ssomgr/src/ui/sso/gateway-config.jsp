<div>
	<form id="frmGateway" />
	<input type="hidden" id="defltGateway" value="${deflt_gateway}" />
	<span class="title">CAS Gateway Configuration<hr /></span>
	<table id="tblGateway" border="0">
		<tr><td style="width:350px" colspan="2" id="tdMsg" class="alertText">&nbsp;</td></tr>
		<tr><td  colspan="2">Choose&nbsp;the&nbsp;default&nbsp;validation&nbsp;service&nbsp;</td></tr>
		<tr>			
			<td class="title" align="right">
				<input type="radio" id="samlGateway" name="authType" value="saml" />SAML Validate
				<input type="radio" id="saml20Gateway" name="authType" value="saml20" />SAML 2.0 Validate
			</td>
			<td class="title" style="padding-top:10px" align="left">
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src="<%=request.getContextPath()%>/ui/css/images/save.png" border="0" id="btnSaveGateway" />
			</td>
		</tr>
	</table>
	</form>
</div>
