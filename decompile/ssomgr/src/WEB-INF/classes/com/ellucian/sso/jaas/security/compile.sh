#!/usr/bin/env bash


# javac -cp '.:../../../../../../lib' JAASLoginModule.java


myClassPath="$(find ../../../../../../lib -iname "*.jar" | tr '\n' ':' | sed 's/.$//')"
Classes="$(find . -iname "*.class" | tr '\n' ':' | sed 's/.$//')"

echo javac -cp $myClassPath JAASRoleGranter.java JAASRolePrincipal.java JAASUserPrincipal.java JAASLoginModule.java
javac -cp $myClassPath JAASRoleGranter.java JAASRolePrincipal.java JAASUserPrincipal.java JAASLoginModule.java
# javac -cp $myClassPath JAASRoleGranter.java JAASRolePrincipal.java JAASUserPrincipal.java

