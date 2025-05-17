#!/usr/bin/env bash


# have to source sdkman even
# if it's been sourced in the parent shell
if [ -f $HOME/.sdkman/bin/sdkman-init.sh ]; then
    source $HOME/.sdkman/bin/sdkman-init.sh
fi

current_jdk="$(sdk list java | grep ">>>" | awk '{ print $(NF) }')"
graal_installed="$(sdk list java | grep graalce | grep installed | head -n 1)"
reset_jdk=false

if [[ "$current_jdk" != *"graalce"* ]]; then
    echo "current jdk is not graal"
    reset_jdk=true
fi

if [ -z "$graal_installed" ]; then
    echo "graal is not installed"
    graal_version="$(sdk list java | grep graalce | head -n 1 | awk '{ print $(NF) }')"
    sdk install java $graal_version
fi

if [ -z "$(which native-image)" ]; then
    echo "native-image binary not found in path"
    exit 1
fi


jbang export native -O DoWorkBin DoWork 


if $reset_jdk ; then
    sdk use java $current_jdk
fi
