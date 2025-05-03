#!/usr/bin/env groovy


// @Grab(group='org.xerial', module='sqlite-jdbc', version='3.47.1.0')
@Grab(group='com.oracle.database.jdbc', module='ojdbc8', version='23.6.0.24.10')
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;


var url = "jdbc:oracle:thin:@banproddb.mtsu.edu:1521:PROD"
var username = "besmith"
var password = "Windowsisawesome#2"


def getConnection(string url, string username, string password) {
        try  {
            DriverManager.registerDriver(new oracle.jdbc.OracleDriver());
        } catch (Exception e) {
            System.err.println("Cannot load the driver, reason:" + e.toString() + "\n" +
                    "Most likely the Java class path is incorrect.");
            System.exit(0);
        }

        // Connect to the database
        Connection conn = null;
        try {
//       System.out.println("Connecting to DB at " + bannerConnectString);
            conn = DriverManager.getConnection(url, user, pass);
        } catch (Exception e) {
            System.err.println("Error connecting to Banner: " + e.getMessage());
        }

        return conn;
}


var conn = getConnection(url, username, password)


var stmt = conn.prepareStatement("select * from spriden where spriden_id = ?");
stmt.setString(1, "M01668100");

var resultSet = stmt.executeQuery();

while (resultSet.next()) {
    System.out.println(resultSet.getString("Spriden_First_Name") + " " + resultSet.getString("Spriden_Last_Name"));
}



