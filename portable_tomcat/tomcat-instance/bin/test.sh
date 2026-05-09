#!/bin/bash
#
USAGE="Usage: $0 {start|stop|status}"
# script to start/stop/status a tomcat instance
#

# if [ -z $WORK_PASSWORD ]; then
	# echo the db password has not been set in the environment
	# echo please run swp to set it
	# exit 1
# fi


# set an instance AppName for use in script/environment 
# this should be set in the .bash_profile
export AppName=test

# set Java environment variable
# JAVA_HOME is using the soft link created to point to the Java Runtime Environment
# this should be set in the .bash_profile
# export JAVA_HOME=/u01/jdk

# set tomcat instance directory
# CATALINA_BASE is the path to the directory of the instance's conf and webapps folders
# this should be set in the .bash_profile
# export CATALINA_BASE=/u01/app/${AppName}

if [ -z $CATALINA_BASE ]; then
	echo CATALINA_BASE is not defined
	exit 1
fi

if [ ! -d $CATALINA_BASE ]; then
	echo CATALINA_BASE directory does not exist
	exit 1
fi

# set tomcat install directory
# CATALINA_HOME is using the soft link created to point to the tomcat software install directory
# this should be set in the .bash_profile
# export CATALINA_HOME=/u01/tomcat

if [ -z $CATALINA_HOME ]; then
	echo CATALINA_HOME is not defined
	exit 1
fi

if [ ! -d $CATALINA_HOME ]; then
	echo CATALINA_HOME directory does not exist
	exit 1
fi

# ensure the environment PATH includes the tomcat instance paths needed 
# this should be set in the .bash_profile
# export PATH=${CATALINA_HOME}:${JAVA_HOME}/bin:${PATH}

# export JAVA_OPTS="";
# export CATALINA_OPTS="";

# use setenv_{instance}.sh to establish the environment variables for this tomcat instance
source $CATALINA_BASE/bin/setenv.sh

# process the parameters
# from the directory of the tomcat instance
# cd ${CATALINA_BASE}

while [[ $* ]]
do
   case "$1" in
        start)
			for logFile in "${CATALINA_BASE}"/logs/*.log "${CATALINA_BASE}"/logs/*.out
			do
				if ! [[ "${logFile##*/}" =~ [0-9] ]]; then
					# echo "Ready to Process: ${logFile%.*} + + ${logFile##*.}"
					# mv ${CATALINA_BASE}/logs/catalina.out ${CATALINA_BASE}/logs/catalina-$(date +"%Y-%m-%d-%p%I%M").out
					echo mv -v ${logFile} ${logFile%.*}-$(date +"%Y-%m-%d-%I%M%p").${logFile##*.}
					mv -v ${logFile} ${logFile%.*}-$(date +"%Y-%m-%d-%I%M%p").${logFile##*.}
				fi
			done

			rm -rf $CATALINA_BASE/temp/*
			rm -rf $CATALINA_BASE/work/*
			echo ""
			echo "Starting Tomcat for Instance: $AppName"
			echo ""
			echo "_______________________________________________________________________________" \
				>> ${CATALINA_BASE}/logs/catalina.out

			echo "                                                                               " \
				>> ${CATALINA_BASE}/logs/catalina.out

			echo " S T A R T I N G     T O M C A T     I N S T A N C E     $AppName" \
				>> ${CATALINA_BASE}/logs/catalina.out

			echo "_______________________________________________________________________________" \
				>> ${CATALINA_BASE}/logs/catalina.out

			${CATALINA_HOME}/bin/startup.sh 2>&1 &
			;;
		stop)
			echo ""
			echo "Shutting down Tomcat for Instance: $AppName"
			echo ""
			echo "_______________________________________________________________________________" \
				>> ${CATALINA_BASE}/logs/catalina.out

			echo "                                                                               " \
				>> ${CATALINA_BASE}/logs/catalina.out

			echo " S T O P P I N G     T O M C A T     I N S T A N C E     $AppName" \
				>> ${CATALINA_BASE}/logs/catalina.out

			echo "_______________________________________________________________________________" \
				>> ${CATALINA_BASE}/logs/catalina.out

			${CATALINA_HOME}/bin/shutdown.sh 5 -force
			;;
		status)
			echo ""
			echo "Checking Tomcat for Instance: $AppName"
			echo ""
			ps -ef | grep -w "\-Dcatalina.base=${CATALINA_BASE}" | grep -v grep
			;;
		-f)
			cd ${CATALINA_BASE}/logs;
			tail -f catalina.out
			;;
		*)
			echo "${USAGE}"
			exit 1
			;;
	esac
	shift
done
exit 0
