#!/usr/bin/env bash


if [ -d $HOME/.tomcat-instance ] || [ -L $HOME/.tomcat-instance ]; then
    echo deploying war

    cp Hello.war $HOME/.tomcat-instance/webapps/
else
    echo "where\'s tomcat?"
fi

