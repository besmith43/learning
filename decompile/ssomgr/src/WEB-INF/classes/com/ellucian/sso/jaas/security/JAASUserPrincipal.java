import java.io.Serializable;
import java.security.Principal;

public class JAASUserPrincipal implements Principal, Serializable {
  private static final long serialVersionUID = -3763971125604861788L;
  
  private String name;
  
  public JAASUserPrincipal(String paramString) {
    if (paramString == null)
      throw new NullPointerException("NULL user name"); 
    this.name = paramString;
  }
  
  public String getName() {
    return this.name;
  }
  
  public String toString() {
    return "UserPrincipal [name=" + this.name + "]";
  }
  
  public int hashCode() {
    null = 1;
    return 31 * null + ((this.name == null) ? 0 : this.name.hashCode());
  }
  
  public boolean equals(Object paramObject) {
    if (this == paramObject)
      return true; 
    if (paramObject == null)
      return false; 
    if (getClass() != paramObject.getClass())
      return false; 
    JAASUserPrincipal jAASUserPrincipal = (JAASUserPrincipal)paramObject;
    if (this.name == null) {
      if (jAASUserPrincipal.name != null)
        return false; 
    } else if (!this.name.equals(jAASUserPrincipal.name)) {
      return false;
    } 
    return true;
  }
}

