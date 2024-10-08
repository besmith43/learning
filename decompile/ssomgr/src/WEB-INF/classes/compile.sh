#!/usr/bin/env bash


# javac -cp '.:../../../../../../lib' JAASLoginModule.java

srcPath="com/ellucian/sso/jaas/security"

myClassPath="$(find ../lib -iname "*.jar" | tr '\n' ':' | sed 's/.$//')"
# Classes="$(find . -iname "*.class" | tr '\n' ':' | sed 's/.$//')"

echo javac -cp $myClassPath $srcPath/JAASRoleGranter.java $srcPath/JAASRolePrincipal.java $srcPath/JAASUserPrincipal.java $srcPath/JAASLoginModule.java
# javac -cp $myClassPath JAASRoleGranter.java JAASRolePrincipal.java JAASUserPrincipal.java JAASLoginModule.java
javac -cp $myClassPath $srcPath/JAASRoleGranter.java $srcPath/JAASRolePrincipal.java $srcPath/JAASUserPrincipal.java $srcPath/JAASLoginModule.java

