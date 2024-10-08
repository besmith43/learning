<%@taglib uri="http://java.sun.com/jsp/jstl/functions" prefix="fn" %>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
<link rel="icon" href="<%=request.getContextPath()%>/ui/css/images/favicon.ico"/>
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
	
	<div id="header">
	    <div id="globalNav">
	      <div>
	        <ul>
	          <c:if test="${adminUserName != null}">
	          <li class="userIdentityText bold">Welcome ${fn:escapeXml(adminUserName)}</li>
			  <li><a class="signOutText pointer" href="<%=request.getContextPath()%>/logout.htm">Sign Out</a></li>
			  <!--li><a class="helpText pointer">Help</a></li-->
			  </c:if>
	        </ul>
	      </div>
	    </div>
    
		<div id="institutionalBranding">
			SSO Manager
		</div>
	
		<div id="menu1">
			<ul class="tabs-nav">
			<li id="tabHome">
				<span>
				<a id="anchorHome" href="<%=request.getContextPath()%>/index.htm"><img 
				src="<%=request.getContextPath()%>/ui/css/images/home-button-icon.png" 
				border="0">&nbsp;Home</img></a>
				</span>
			</li>
			<li id="tabInb">
				<span>
				<a id="anchorInb" href="<%=request.getContextPath()%>/inb/view-config.htm">INB Configuration</a>
				</span>
			</li>
			<li id="tabSsb">
				<span>
				<a id="anchorSsb" href="<%=request.getContextPath()%>/ssb/view-config.htm">SSB Configuration</a>
				</span>
			</li>
			</ul>
		</div>
		 
	</div>
	
  	<div id="helpWindow">
	    <div id="helpWindowControls">
	      <span id="helpWindowIcon"></span> 
	      <span id="helpWindowTitle"></span> 
	      <span id="helpWindowCloseButton"></span>
	    </div>
	
	    <div id="helpWindowTop"></div>
	
	    <div id="helpWindowMiddle">
	      <iframe id="helpWindowContent" frameborder="no"></iframe>
	    </div>
	
	    <div id="helpWindowBottom"></div><span id="helpWindowTab"><span 
	    id="helpWindowTabText"></span></span>
 	</div>
