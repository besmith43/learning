package com.ellucian.sso.jaas.security;


import java.io.Serializable;
import java.security.Principal;

public class JAASRolePrincipal implements Principal, Serializable {
  private static final long serialVersionUID = -7343730989684430460L;
  
  private String name;
  
  public String getName() {
    return this.name;
  }
  
  public JAASRolePrincipal(String paramString) {
    if (paramString == null)
      throw new NullPointerException("NULL user name"); 
    this.name = paramString;
  }
  
  public String toString() {
    return "RolePrincipal [name=" + this.name + "]";
  }
  
  public int hashCode() {
    // null = 1;
    // return 31 * null + ((this.name == null) ? 0 : this.name.hashCode());
	return this.name.hashCode();
  }
  
  public boolean equals(Object paramObject) {
    if (this == paramObject)
      return true; 
    if (paramObject == null)
      return false; 
    if (getClass() != paramObject.getClass())
      return false; 
    JAASRolePrincipal jAASRolePrincipal = (JAASRolePrincipal)paramObject;
    if (this.name == null) {
      if (jAASRolePrincipal.name != null)
        return false; 
    } else if (!this.name.equals(jAASRolePrincipal.name)) {
      return false;
    } 
    return true;
  }
}

