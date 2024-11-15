<%--
  Created by IntelliJ IDEA.
  User: besmith
  Date: 11/5/24
  Time: 7:52 PM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Method Test</title>
</head>
<body>
    <p>You can only perform GET's and POST's against JSP's</p>
  <%
    out.println(request.getMethod());
  %>
</body>
</html>
