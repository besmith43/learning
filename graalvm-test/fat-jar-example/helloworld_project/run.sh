#!/usr/bin/env bash

mvn clean && mvn package

java -cp lib/mail.jar:lib/activation-1.1.1.jar -jar target/helloworld-1.0-SNAPSHOT.jar
