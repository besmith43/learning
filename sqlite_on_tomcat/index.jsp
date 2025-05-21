
<%@page import="MyPackage.Hello" %>
<%@page import="javax.sql.DataSource" %>
<%@page import="javax.naming.Context" %>
<%@page import="javax.naming.InitialContext" %>
<%@page import="javax.naming.NamingEnumeration" %>
<%@page import="javax.naming.NameClassPair" %>
<%@page import="java.sql.Connection" %>
<%@page import="java.sql.ResultSet" %>
<%@page import="java.sql.Statement" %>
<%@page import="java.util.Date" %>
<%@page import="javax.naming.InitialContext" %>
<%@page import="javax.sql.DataSource" %>


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
			String DATASOURCE = "java:/comp/env/jdbc/person";

			Context ctx = new InitialContext();

			NamingEnumeration<NameClassPair> list = ctx.list("java:/comp/env/jdbc/");
			while (list.hasMore()) {
				out.println("Context List Item: ");
				out.println(list.next().getName());
				out.println("<br>");
			}

			DataSource ds = (DataSource) ctx.lookup(DATASOURCE);

            String str1 = "";
            Connection connection = null;
            try {
                connection = ds.getConnection();
            } catch (Exception exception) {
                out.println("Cannot load the data resource " + DATASOURCE + ", reason:" + exception.toString());
            } 
            if (connection != null) {
                String str = "select * from person where firstname = 'bob'";
                try {
                    Statement statement = connection.createStatement();
                    ResultSet resultSet = statement.executeQuery(str);
                    while (resultSet.next())
                        str1 = resultSet.getString("lastname"); 
                        resultSet.close();
                        statement.close();
                    } catch (Exception exception) {
                        out.println("Sql Query Failed: " + exception.toString());
                } 
                try {
                    connection.close();
                    connection = null;
                } catch (Exception exception) {
                    out.println("Close DB Connection Failed: " + exception.toString());
                }
            } 
			out.println("Hello " + str1);
		%></p>
	</body>
</html>
