$(document).bind("ready", function() {	
	$("#btnSaveSsb").bind("click", function() {
		$("#frmSsb").attr("action", $("#contextPath").val() + "/ssb/update-config.htm");
		$("#frmSsb").attr("method", "POST");
		$("#frmSsb").submit();
	});
	$("#btnSaveInb").bind("click", function() {
		$("#frmInb").attr("action", $("#contextPath").val() + "/inb/update-config.htm");
		$("#frmInb").attr("method", "POST");
		$("#frmInb").submit();
	});
	$("#btnCnclSsb, #btnCnclInb").bind("click", function() {
		$("#frmSsb").attr("action", $("#contextPath").val() + "/index.htm");
		$("#frmSsb").attr("method", "POST");
		$("#frmSsb").submit();
	});
	$("#btnCnclInb").bind("click", function() {
		$("#frmInb").attr("action", $("#contextPath").val() + "/index.htm");
		$("#frmInb").attr("method", "POST");
		$("#frmInb").submit();
	});
	$("#btnSvOraclePwd").bind("click", function() {
		return redirect($("#ssoMgrOraclePwdValidateUrl").val());
	});
	$("#btnCnclOraclePwd").bind("click", function() {
		$("#frmPwdPrmpt").attr("action", $("#cancelUrl").val());
		$("#frmPwdPrmpt").submit();
	});
	$("#frmSsb #casMode, #frmSsb #authMode, #frmSsb #SamlMode, #frmInb #casMode, #frmInb #SamlMode, #frmInb #authMode").bind("click", function() {
		return toggleModeFields();
	});
	$("#frmInb #prmptPolicy, #frmInb #genPolicy").bind("click", function() {
		return togglePwdFields();
	});
	$("#btnFlshInb").bind("click", function() {
		return deleteCredentials();
	});
	$("#btnFlshInb,#btnSaveInb,#btnCnclInb,#btnSaveSsb,#btnCnclSsb,#btnSvOraclePwd,#btnCnclOraclePwd,#btnSaveGateway").bind("mouseover", function() {
		$(this).css('cursor','pointer'); 
	});
	$("#btnSaveGateway").bind("click", function() {
		return changeGateway();
	});
	$("#frmPwdPrmpt #ssoMgrOraclePwd").keydown(function (e) {
		if (e.keyCode == 13) {
			e.preventDefault();         
			return false;     
		} 
	});
	$("#frmSsb #dlink").click("click", function() {
		return toggleTxtFields();
	});	
	init();
});
function init() {
	if($("#page-id").length) {
		if($("#page-id").val() == "banner-gateway" && $("#defltGateway").val() == "saml20") {
			$("#tabHome").addClass("selectedmenu");
		} else if($("#page-id").val() == "saml-gateway" && $("#defltGateway").val() == "saml") {
			$("#tabHome").addClass("selectedmenu");
		} else if($("#page-id").val() == "ssb") {
			$("#tabSsb").addClass("selectedmenu");
		} else if($("#page-id").val() == "inb") {
			$("#tabInb").addClass("selectedmenu");
		}
	}
	if($("#frmSsb #casMode").length && $("#frmSsb #authMode").length || $("#frmSsb #SamlMode").length) {   
		toggleModeFields();
	}
	if($("#frmInb #casMode").length && $("#frmInb #authMode").length || $("#frmInb #SamlMode").length) {   
		toggleModeFields();
	}
	if($("#frmInb #prmptPolicy").length && $("#frmInb #genPolicy").length) {
		togglePwdFields();
	}
	if($("#frmPwdPrmpt #ssoMgrOraclePwd").length) {
		$("#frmPwdPrmpt #ssoMgrOraclePwd").focus();
	}
	if($("#frmGateway #defltGateway").length && $("#frmGateway #defltGateway").length) {
		toggleGateway();
	}
	if($("#frmSsb #baseUrl").length && $("#frmSsb #urlParameterName").length) {
		toggleTxtFieldsState();
	}
}
function toggleModeFields() {
	if($('#casMode').is(':checked')) { 
		$("#udcIdIndicator").prop("disabled", true);
		$("#udcIdKey").prop("disabled", true);
	} else if($('#authMode').is(':checked')) {
		$("#udcIdIndicator").prop("disabled", false);
		$("#udcIdKey").prop("disabled", false);
	}else if($('#SamlMode').is(':checked')) {
		$("#udcIdIndicator").prop("disabled", false);
		$("#udcIdKey").prop("disabled", false);
	}
}
function togglePwdFields()
{
	if($("#prmptPolicy").is(':checked')) {
		$("#pwdValidChars").prop("disabled", true);
		$("#pwdMinLength").prop("disabled", true);
		$("#pwdMaxLength").prop("disabled", true);
	} else if($("#genPolicy").is(':checked')) {
		$("#pwdValidChars").prop("disabled", false);
		$("#pwdMinLength").prop("disabled", false);
		$("#pwdMaxLength").prop("disabled", false);
	}
}
function redirect(url) {
	$("#frmPwdPrmpt #tdMsg").html("Please wait while the password is being validated...");
	if(validatePwd()) {
		$.ajax ({ url: url,
			type: "POST",
			data: { ssoMgrOracleUserId: $("#ssoMgrOracleUserId").val(), 
				ssoMgrOraclePwd: $("#ssoMgrOraclePwd").val() },
			dataType: "json", 
			success: showUIMsg,
			error: errorMsg });
	}
}
function showUIMsg(jsonData) {
	if(jsonData.result == "success") {
		var url = $("#ssoMgrInbHandlerUrl").val();
		$("#frmPwdPrmpt #tdMsg").html("The password has been successfully validated. Pls wait...");
		$("#frmPwdPrmpt").attr("action", url);
		$("#frmPwdPrmpt").attr("method", "POST");
		$("#frmPwdPrmpt").submit();
	} else {
		$("#frmPwdPrmpt #tdMsg").html("The password is incorrect. Please try again");
		$("#frmPwdPrmpt #ssoMgrOraclePwd").focus();
	}
}
function errorMsg() { 
	$("#frmPwdPrmpt #tdMsg").html("An error occured during password validation. Please try again");	
}
function validatePwd()
{
	if($("#ssoMgrOraclePwd").val().length <= 0) {
		$("#frmPwdPrmpt #tdMsg").html("The password is required");
		$("#frmPwdPrmpt #ssoMgrOraclePwd").focus();
		return false;
	}
	return true;
}
function deleteCredentials() {
	if(confirm("Are you sure you want to delete the user credentials?")) {
		showMsg($("#frmInb #tdMsg"), "Please wait while the credentials are being deleted...");
		$.ajax ({ url: $("#contextPath").val() + "/inb/delete-credentials.htm",
			type: "POST",
			dataType: "json",
			success: credMsg,
			error: errorCredMsg });
	}
}
function credMsg(jsonData) {
	if(jsonData.result == "success") {
		showMsg($("#frmInb #tdMsg"), "Internet Native Banner user credentials have been deleted");
	} else {
		errorCredMsg();
	}
}
function errorCredMsg() { 
	showMsg($("#frmInb #tdMsg"), "An error occured while deleting credentials. Please try again");
	$("#frmInb #btnFlshInb").focus();
}
function showMsg(node, msg) {
	node.html(msg);
}
function toggleGateway() {
	if($("#defltGateway").val() == "saml") {
		$("#samlGateway").prop("checked", true);
	} else if($("#defltGateway").val() == "saml20") {
		$("#saml20Gateway").prop("checked", true);
	}
}
function changeGateway() {
	if($("#tolerance").val() < 1 || $("#tolerance").val() > 1440) {
		$("#frmGateway #tdMsg").html("Please enter a number between 1 to 1440 for the Time Tolerance");
		$("#tolerance").select();
		return;
	}
	$("#frmGateway #tdMsg").html("Please wait while the CAS gateway configuration is being updated...");
	var url = $("#contextPath").val() + "/gateway-config.htm";
	$.ajax ({ url: url,
		type: "POST",
		data: { authType: $("#frmGateway :checked").val() 
		// , tolerance: $("#tolerance").val() 
		}, 
		dataType: "json", 
		success: showGatewayMsg,
		error: errorGatewayMsg });
}
function showGatewayMsg(jsonData) {
	if(jsonData.result == "success") {
		showMsg($("#frmGateway #tdMsg"), "The CAS gateway configuration has been updated");
	} else {
		errorGatewayMsg();
	}
}
function errorGatewayMsg() { 
	showMsg($("#frmGateway #tdMsg"), "An error occured while updating the CAS gateway configuration. Please try again");
	$("#frmGateway #btnSaveGateway").focus();
}

function toggleTxtFieldsState() {
	if($("#frmSsb #dlink").is(':checked')) {
		$("#frmSsb #baseUrl").prop("disabled", false);
		$("#frmSsb #urlParameterName").prop("disabled", false);
	} else {
		$("#frmSsb #baseUrl").prop("disabled", true);
		$("#frmSsb #urlParameterName").prop("disabled", true);
	}
}
function toggleTxtFields() {
	if($("#frmSsb #dlink").is(':checked')) {
		$("#frmSsb #baseUrl").prop("disabled", false);
		$("#frmSsb #urlParameterName").prop("disabled", false);
		if($.trim($("#frmSsb #baseUrl").val()).length <= 0) {
			$("#frmSsb #baseUrl").attr("value","http://"); 
		}
		if($.trim($("#frmSsb #urlParameterName").val()).length <= 0) {
			$("#frmSsb #urlParameterName").attr("value","pkg");
		}
	} else {
		$("#frmSsb #baseUrl").prop("disabled", true);
		$("#frmSsb #urlParameterName").prop("disabled", true);
	}
}

