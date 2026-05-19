#!/bin/bash
#
# set runtime options for tomcat instance
#

##
## Nick suggessted this might be needed Ellucian Software Management, but these do not have a shared_configuration
## $BANNER_HOME/bin/setenv.sh 
## .... added the following -  
## JAVA_OPTS="$JAVA_OPTS -DBANNER_COMMUNICATION_MANAGEMENT_SSB_APP=/u01/app/banner/TEST/shared_configuration/CommunicationManagement_configuration.groovy"
##

##
## Theresa S suggested this... Joey – with the war files I’ve touched from R2, I had to add some lines to the 
## $BANNER_HOME/bin/setenv.sh file.  You may need to add the following if you haven’t already:
## JAVA_OPTS="$JAVA_OPTS -DBANNER_APP_CONFIG=/u01/app/EthosApi/shared_configuration/banner_configuration.groovy"
## JAVA_OPTS="$JAVA_OPTS -BANNER_INTEGRATION_API_CONFIG=/u01/app/EthosApi/shared_configuration/IntegrationApi_configuration.groovy"
## JAVA_OPTS="$JAVA_OPTS -BANNER_STUDENT_API_CONFIG=/u01/app/EthosApi/shared_configuration/StudentApi_configuration.groovy"
##
## Also, that shared_configuration directory (bold above) has to be created and the configuration.groovy files placed in that folder.  
## For the applicationNavigator and BannerExtensibility, after I made those changes, the war file deployed.  
## I’m not familiar with the api war files – but thought I’d mention this in case you hadn’t already run across this.
##

#
# This allows the use of shutdown.sh -force
export CATALINA_PID="${CATALINA_BASE}/bin/catalina.pid"

# deprecated options that I've removed
# -XX:+UseConcMarkSweepGC \
# -XX:+UseParNewGC \
# -XX:+UseCMSInitiatingOccupancyOnly \
# -XX:CMSInitiatingOccupancyFraction=80 \
# -Djdk.tls.ephemeralDHKeySize=2048 \

export JAVA_OPTS=" \
 -server -Xms512m -Xmx4g \
 -Djava.security.egd=file:/dev/./urandom \
 -Djava.rim.server.hostname=$HOSTNAME \
 -Djava.util.logging.manager=org.apache.juli.ClassLoaderLogManager \
 -Djava.util.logging.SimpleFormatter.format=\"[%1\\\$tY-%1\\\$tm-%1\\\$td %1\\\$tH:%1\\\$tM:%1\\\$tS.%1\\\$tL %1\\\$tz\$s],[%4\\\$s],[%2\\\$s],[%5\\\$s],[%6\\\$s]%n\" \
 -Djava.awt.headless=true \
 -XX:ReservedCodeCacheSize=128m \
 -XX:+DisableExplicitGC \
 -Djava.protocol.handler.pkgs=org.apache.catalina.webresources \
 -Dorg.apache.catalina.security.SecurityListener.UMASK=0027 \
 -Djava.net.preferIPv4Stack=true \
 -Dats-tengine-aio.host=127.0.0.1 \
 -Dats-shared-fs.host=127.0.0.1 \
 -Dignore.endorsed.dirs= \
 -classpath ${CATALINA_HOME}/bin/bootstrap.jar:${CATALINA_HOME}/bin/tomcat-juli.jar \
 -Dbanner.logging.dir=${CATALINA_BASE}/logs \
 -DBANNER_APP_CONFIG=${CATALINA_BASE}/shared_configuration/banner_configuration.groovy \
 -DBANNER_INTEGRATION_API_CONFIG=${CATALINA_BASE}/shared_configuration/IntegrationApi_configuration.groovy \
 -DBANNER_STUDENT_API_CONFIG=${CATALINA_BASE}/shared_configuration/StudentApi_configuration.groovy \
 -Dcom.sun.management.jmxremote -Dcom.sun.management.jmxremote.port=1099 -Dcom.sun.management.jmxremote.ssl=false -Dcom.sun.management.jmxremote.authenticate=false \
"

#
# -Djava.util.logging.SimpleFormatter.format=\"%1\\\$tY-%1\\\$tm-%1\\\$td %1\\\$tH:%1\\\$tM:%1\\\$tS.%1\\\$tL %4\\\$s %3\#\\$s %5\\\$s%6\\\$s%n\" \
# -Doracle.jdbc.autoCommitSpecCompliant=true \
# -verbose:gc \

# -Doom.sun.management.jmxremote \
# -Dcom.sun.management.remote.port=1099 \
# -Dcom.sun.management.remote.ssl=false \
# -Dcom.sun.management.remote.ssl=authenticate=false \
# -Doracle.jdbc.autoCommitSpecCompliant=false \
# -Dapp.env=dev \
# -Djava.rmi.server.hostname=localhost \
# -Dcom.sun.management.jmxremote.host=localhost \

export CATALINA_OPTS="$CATALINA_OPTS \
 -Duser.timezone=America/Chicago \
"

#
# was originally -server -Xms6g -Xmx6g \\ this is to pre-allocate all memory that would be used 
# trying         -server -Xms2g -Xmx4g \\ this is to avoid memory leaks due to maxed memory pool
#
# ,\[%4\\\$s\],\[%2\\\$s\],\[%5\\\$s\],\[%6\\\$s\]%n\" \
# [%1$tY-%1$tm-%1$td %1$tH:%1$tM:%1$tS.%1$tL %1$tz],[%4$s],[%2$s],[%5$s],[%6$s]%n

