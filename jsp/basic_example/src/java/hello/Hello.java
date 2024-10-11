package MyPackage;

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


	public String Greeting() {
		String Name = "Bob";
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
			String query = "select spriden_first_name from spriden where spriden_id = 'M01668100'";

			try {
				Statement stmt = connection.createStatement();
				ResultSet rset = stmt.executeQuery(query);

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

	public String Greeting(DataSource ds) {
		String Name = "Bob";
		Connection connection = null;
		String DATASOURCE = "java:/comp/env/jdbc/BannerTest";

        try {
            // System.err.println("Hello Class: Loading the data resource " + DATASOURCE);
            connection = ds.getConnection();

		} catch (Exception e) {
			doError("Cannot load the data resource " + DATASOURCE + ", reason:" + e.toString());
		}

		if (connection != null) {
			String query = "select spriden_first_name from spriden where spriden_id = 'M01668100'";

			try {
				Statement stmt = connection.createStatement();
				ResultSet rset = stmt.executeQuery(query);

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

    // Severe error method.
    private void doError(String errorLine) {
        Date nowDate = new Date();
        System.err.println("Hello Class at time " + nowDate.toString() + " error occurred:");
        if (errorLine != null) {
            System.err.println("Hello Class: Error - " + errorLine);
        }
    }// end doError method
}



