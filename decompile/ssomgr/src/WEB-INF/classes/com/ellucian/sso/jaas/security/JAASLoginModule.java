package com.ellucian.sso.jaas.security;


// import com.ellucian.sso.jaas.security.JAASRolePrincipal;
// import com.ellucian.sso.jaas.security.JAASUserPrincipal;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import javax.security.auth.Subject;
import javax.security.auth.callback.Callback;
import javax.security.auth.callback.CallbackHandler;
import javax.security.auth.callback.NameCallback;
import javax.security.auth.callback.PasswordCallback;
import javax.security.auth.login.LoginException;
import javax.security.auth.spi.LoginModule;
import org.apache.log4j.Logger;

public class JAASLoginModule implements LoginModule {
  private static Logger LOGGER = Logger.getLogger(JAASLoginModule.class);
  
  private Subject subject;
  
  private CallbackHandler callbackHandler;
  
  private Map sharedState;
  
  private Map options;
  
  private boolean debug = false;
  
  private boolean succeeded = false;
  
  private boolean commitSucceeded = false;
  
  private String username = null;
  
  private String password = null;
  
  private JAASUserPrincipal userPrincipal = null;
  
  public void initialize(Subject paramSubject, CallbackHandler paramCallbackHandler, Map<String, ?> paramMap1, Map<String, ?> paramMap2) {
    this.subject = paramSubject;
    this.sharedState = paramMap1;
    this.options = paramMap2;
    this.debug = "true".equalsIgnoreCase((String)paramMap2.get("debug"));
    try {
      NameCallback nameCallback = new NameCallback("prompt");
      PasswordCallback passwordCallback = new PasswordCallback("prompt", false);
      paramCallbackHandler.handle(new Callback[] { nameCallback, passwordCallback });
      this.password = new String(passwordCallback.getPassword());
      this.username = nameCallback.getName();
    } catch (Exception exception) {
      throw new RuntimeException(exception);
    } 
  }
  
  public boolean login() throws LoginException {
    try {
      if (this.debug) {
        LOGGER.debug("Username :" + this.username);
        LOGGER.debug("Password : " + this.password);
      } 
      if (this.username == null || this.password == null) {
        LOGGER.error("Callback handler does not return login data properly");
        throw new LoginException("Callback handler does not return login data properly");
      } 
      if (isValidUser()) {
        this.succeeded = true;
        return true;
      } 
    } catch (Exception exception) {
      exception.printStackTrace();
    } 
    return false;
  }
  
  public boolean commit() throws LoginException {
    if (!this.succeeded)
      return false; 
    this.userPrincipal = new JAASUserPrincipal(this.username);
    if (!this.subject.getPrincipals().contains(this.userPrincipal)) {
      this.subject.getPrincipals().add(this.userPrincipal);
      LOGGER.debug("User principal added:" + this.userPrincipal);
    } 
    List<String> list = getRoles();
    for (String str : list) {
      JAASRolePrincipal jAASRolePrincipal = new JAASRolePrincipal(str);
      if (!this.subject.getPrincipals().contains(jAASRolePrincipal)) {
        this.subject.getPrincipals().add(jAASRolePrincipal);
        LOGGER.debug("Role principal added: " + jAASRolePrincipal);
      } 
    } 
    this.commitSucceeded = true;
    LOGGER.info("Login subject were successfully populated with principals and roles");
    return true;
  }
  
  public boolean abort() throws LoginException {
    if (!this.succeeded)
      return false; 
    if (this.succeeded == true && !this.commitSucceeded) {
      this.succeeded = false;
      this.username = null;
      if (this.password != null)
        this.password = null; 
      this.userPrincipal = null;
    } else {
      logout();
    } 
    return true;
  }
  
  public boolean logout() throws LoginException {
    this.subject.getPrincipals().remove(this.userPrincipal);
    this.succeeded = false;
    this.succeeded = this.commitSucceeded;
    this.username = null;
    this.password = null;
    this.userPrincipal = null;
    return true;
  }
  
  private boolean isValidUser() throws LoginException {
    String str = (String)this.options.get("userQuery");
    Connection connection = null;
    ResultSet resultSet = null;
    PreparedStatement preparedStatement = null;
    boolean bool1 = false;
    boolean bool2 = false;
    try {
      connection = getConnection();
      LOGGER.debug("The connection object is " + connection);
      if (connection != null) {
        bool1 = true;
        preparedStatement = connection.prepareStatement(str);
        preparedStatement.setString(1, this.username);
        resultSet = preparedStatement.executeQuery();
        while (resultSet.next()) {
          String str1 = resultSet.getString(1);
          if ("YES".equalsIgnoreCase(str1))
            bool2 = true; 
        } 
      } 
      if (bool1 && !bool2)
        LOGGER.info(this.username + " does not have access to BEIS application"); 
    } catch (Exception exception) {
      LOGGER.error("Error when loading user from the database " + exception);
      exception.printStackTrace();
    } finally {
      try {
        if (resultSet != null)
          resultSet.close(); 
      } catch (SQLException sQLException) {
        LOGGER.error("Error when closing result set." + sQLException);
      } 
      try {
        if (preparedStatement != null)
          preparedStatement.close(); 
      } catch (SQLException sQLException) {
        LOGGER.error("Error when closing statement." + sQLException);
      } 
      try {
        if (connection != null)
          connection.close(); 
      } catch (SQLException sQLException) {
        LOGGER.error("Error when closing connection." + sQLException);
      } 
    } 
    return bool2;
  }
  
  private List<String> getRoles() {
    ArrayList<String> arrayList = new ArrayList();
    arrayList.add("admin");
    return arrayList;
  }
  
  private Connection getConnection() throws LoginException {
    String str1 = (String)this.options.get("dbURL");
    String str2 = (String)this.options.get("dbDriver");
    Connection connection = null;
    try {
      Class.forName(str2).newInstance();
      connection = DriverManager.getConnection(str1, this.username, this.password);
    } catch (Exception exception) {
      LOGGER.error("Error when creating database connection" + exception + "\n" +
        "dbURL: " + str1 + "\n" +
        "dbDriver: " + str2 + "\n" + 
        "Username: " + this.username + "\n"
      );
      exception.printStackTrace();
    } finally {}
    return connection;
  }
}

