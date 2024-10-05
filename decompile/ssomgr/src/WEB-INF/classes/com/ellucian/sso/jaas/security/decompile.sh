#!/usr/bin/env bash

java -jar ~/Developer/Personal/learning-java/decompile/jd-cli/jd-cli.jar JAASLoginModule.class > JAASLoginModule.java
java -jar ~/Developer/Personal/learning-java/decompile/jd-cli/jd-cli.jar JAASRoleGranter.class > JAASRoleGranter.java
java -jar ~/Developer/Personal/learning-java/decompile/jd-cli/jd-cli.jar JAASRolePrincipal.class > JAASRolePrincipal.java
java -jar ~/Developer/Personal/learning-java/decompile/jd-cli/jd-cli.jar JAASUserPrincipal.class > JAASUserPrincipal.java

