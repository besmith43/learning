package hello;

import java.sql.*;
import javax.sql.*;

// import java.sql.Connection;
// import java.sql.DriverManager;
// import java.sql.PreparedStatement;
// import java.sql.ResultSet;
// import java.sql.SQLException;
import javax.naming.Context;
import javax.naming.InitialContext;
// import javax.naming.NamingEnumeration;
// import javax.naming.NameClassPair;
import java.util.Date;



public class Hello {


    public static String Greeting() {
        String Name = "Bob";
        Connection connection = null;
        String DATASOURCE = "java:/comp/env/jdbc/BannerTest";
        DataSource ds;
        String MNumber = "M01668100";

        try {
            Context ctx = new InitialContext();
            ds = (DataSource) ctx.lookup(DATASOURCE);
            // System.err.println("Hello Class: Loading the data resource " + DATASOURCE);
            connection = ds.getConnection();

        } catch (Exception e) {
            doError("Cannot load the data resource " + DATASOURCE + ", reason:" + e.toString());
        }

        if (connection != null) {
//            String query = "select spriden_first_name from spriden where spriden_id = 'M01668100'";
            String query = "select spriden_first_name from spriden where spriden_id = ?";

            try {
//                Statement stmt = connection.createStatement();
//                ResultSet rset = stmt.executeQuery(query);

                PreparedStatement stmt = connection.prepareStatement(query);
                stmt.setString(1, MNumber);
                ResultSet rset = stmt.executeQuery();


                while (rset.next()) {
                    Name = rset.getString("spriden_first_name");
                }

                rset.close();
                stmt.close();
            } catch (Exception e) {
                doError("Sql Query Failed: " + e.toString());
            }

            try {
                connection.close();
                connection = null;
            } catch (Exception e) {
                doError("Close DB Connection Failed: " + e.toString());
            }
        }

        return "Hello " + Name;
    }

    /**
     * @return string
     */
    public static String PermissionCheck() {
        String answer = "NO";
        String user = "BESMITH";
        String query = "SELECT BANSECR.G$_AUTHORIZATION_PKG.g$_check_authorization_fnc('BEIS_ADMIN_OBJECT', ?) FROM DUAL";
        Connection connection = null;
        String DATASOURCE = "java:/comp/env/jdbc/BannerTest";
        DataSource ds;
        try {
            Context ctx = new InitialContext();
            ds = (DataSource) ctx.lookup(DATASOURCE);
            // System.err.println("Hello Class: Loading the data resource " + DATASOURCE);
            connection = ds.getConnection();

        } catch (Exception e) {
            doError("Cannot load the data resource " + DATASOURCE + ", reason:" + e.toString());
        }

        if (connection != null) {
            try {
//                Statement stmt = connection.createStatement();
//                ResultSet rset = stmt.executeQuery(query);

                PreparedStatement stmt = connection.prepareStatement(query);
                stmt.setString(1, user);
                ResultSet rset = stmt.executeQuery();


                while (rset.next()) {
                    answer = rset.getString(1);
                }

                rset.close();
                stmt.close();
            } catch (Exception e) {
                doError("Sql Query Failed: " + e.toString());
            }

            try {
                connection.close();
                connection = null;
            } catch (Exception e) {
                doError("Close DB Connection Failed: " + e.toString());
            }
        }

        return answer;
    }


    // Severe error method.
    private static void doError(String errorLine) {
        Date nowDate = new Date();
        System.err.println("Hello Class at time " + nowDate.toString() + " error occurred:");
        if (errorLine != null) {
            System.err.println("Hello Class: Error - " + errorLine);
        }
    }// end doError method
}



