#!/usr/bin/env bash


if [ -z "$(ps -ef | grep Dcatalina.base | grep -v grep)" ]; then
    echo tomcat isn\'t running
    exit 1
fi


./build.sh && \
./deploy.sh && \
echo sleeping for 10 seconds && \
sleep 10 && \
echo && \
echo && \
curl localhost:8080/MyServlet/api && \
echo && \
echo && \
curl localhost:8080/MyServlet/hello && \
echo && \

curl \
--header "Content-Type: application/json" \
--request POST \
--data '{"name":"post request example","value":789}' http://localhost:8080/MyServlet/api && \
echo

