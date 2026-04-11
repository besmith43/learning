
<%@page import="MyPackage.Hello" %>
<%@page import="javax.sql.DataSource" %>
<%@page import="javax.naming.Context" %>
<%@page import="javax.naming.InitialContext" %>
<%@page import="javax.naming.NamingEnumeration" %>
<%@page import="javax.naming.NameClassPair" %>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>Hello!</title>
    </head>
    <body>
        <h1>Hello World! from jsp source-code</h1>

		<p><%
			Context ctx = new InitialContext();

			NamingEnumeration<NameClassPair> list = ctx.list("java:/comp/env/jdbc/");
			while (list.hasMore()) {
				out.println("Context List Item: ");
				out.println(list.next().getName());
				out.println("<br>");
			}

			Hello hello = new Hello();
			out.println("<br>");
			out.println(hello.Greeting());
			out.println("<br>");
		%></p>
	</body>
</html>
