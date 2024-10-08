package com.ellucian.sso.jaas.security;

import java.security.Principal;
import java.util.Collections;
import java.util.Set;
import org.springframework.security.authentication.jaas.AuthorityGranter;

public class JAASRoleGranter implements AuthorityGranter {
  public Set<String> grant(Principal paramPrincipal) {
    return paramPrincipal.getName().equals("admin") ? Collections.singleton("ROLE_ADMIN") : null;
  }
}

