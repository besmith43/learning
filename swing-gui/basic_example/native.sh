#!/usr/bin/env bash -i
# needing the -i to ensure that my .bashrc is loaded
# when the subshell that's running this script is called
# I'm feeling lazy

current_jdk=""

if [ -z "$(which native-image)" ] && [ "$(type -t sdk)" == "function" ]; then

    current_jdk="$(sdk list java | grep -i ">>>" | awk '{ print $(NF) }')"

    if [ -n "$current_jdk" ]; then
        echo "found current jdk: $current_jdk"
    fi

    graal_installed_version="$(sdk list java | grep -i graalce | grep -i installed | head -n 1 | awk '{ print $(NF) }')"

    if [ -n "$graal_installed_version" ]; then
        echo graalvm installed version found $graal_installed_version
        sdk use java "$graal_installed_version"
    else
        graal_latest_version="$(sdk list java | grep -i graalce | head -n 1 | awk '{ print $(NF) }')"

        if [ -n "$graal_latest_version" ]; then
            echo graalvm latest version found $graal_latest_version
            sdk install java "$graal_latest_version"
        fi
    fi

    # exit
elif [ -z "$(which native-image)" ]; then
    echo "you need to be using graalvm"
    exit 1
fi


jbang export native -O hello hello.java


if [ -n "$current_jdk" ]; then
    echo "resetting back to original jdk: $current_jdk"
    sdk use java "$current_jdk"
fi


