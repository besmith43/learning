<%@ page language="java" %>
<%@ page import="org.springframework.security.core.AuthenticationException" %>
<!DOCTYPE html>
<html>
<head>
    <title></title>
    <style type="text/css">
        BODY, TABLE, TH, TR, TD, SPAN, h1, h2, h3, h4, h5, h6 {color: #000000; font-family: verdana, helvetica, arial, sans-serif;}
        BODY { background-color: #ffffff; }
        .head24 {font-size: 1.5em; font-weight: normal;}
        .bgwarning {background-color: #000000;}
        .bgslight {background-color: #efefef;}
        .text12 {color: #000000; font-size: .85em;}
    </style>
</head>

<body leftmargin="8" topmargin="8" marginwidth="8" marginheight="8">


<center>
<table cellpadding="0" cellspacing="0" border="0" width="450">
    <tr><td></td></tr>
    <tr><td class="head24" id="section_head_txt">&nbsp; Authentication Failure</td></tr>
    <tr><td></td></tr>
    <tr>
        <td class="bgwarning">
            <table cellpadding="2" cellspacing="0" border="0" width="100%" class="bgwarning">
                <tr>
                    <td>
                        <table cellpadding="20" cellspacing="0" border="0" width="100%" class="bgslight">
                            <tr>
                                <td>
                                    <span class="text12" id="msg_txt">
                                    <B>Single Sign-On Failed</B><P>
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr><td></td></tr>
                                            <tr><td>
                                                <span class="text12">
                                                    <%
                                                        AuthenticationException key = (AuthenticationException)request.getSession().getAttribute("SPRING_SECURITY_LAST_EXCEPTION");
                                                        if(key != null && !"".equals(key)){
                                                    %>
                                                    <%=key.getMessage()%>
                                                    <% } %>
                                                </span><br>
                                            </td></tr>
                                            <tr><td>
                                                <span class="text12">
                                                Please contact your system administrator.
                                                </span>
                                            </td></tr>
                                        </table>
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</center>
</body>
</html>
