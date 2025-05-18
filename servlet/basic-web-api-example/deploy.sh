#!/usr/bin/env bash


if [ -d $HOME/.tomcat-instance ] || [ -L $HOME/.tomcat-instance ]; then
    echo deploying war

    cp MyServlet.war $HOME/.tomcat-instance/webapps/
else
    echo "where\'s tomcat?"
fi

