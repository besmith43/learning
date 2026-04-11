#!/usr/bin/env bash



test -d ./.tomcat-core && rm -r ./.tomcat-core || echo "nothing's there"
curl https://dlcdn.apache.org/tomcat/tomcat-9/v9.0.116/bin/apache-tomcat-9.0.116.tar.gz -o ./tomcat-9.tar.gz || exit 1
tar -xvf ./tomcat-9.tar.gz && rm ./tomcat-9.tar.gz || exit 1
mv ./apache-tomcat-9.0.116 ./.tomcat-core

