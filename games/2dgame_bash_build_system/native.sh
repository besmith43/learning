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


if [ -f 2dgame.jar ]; then
    rm 2dgame.jar
fi

if [ -d target ]; then
    rm -r target
fi

mkdir target

javac -d target src/main/java/main/*.java src/main/java/**/*.java

if [ $? -ne 0 ]; then
    echo "java compile failed"
    exit 1
fi

cp -r src/main/resources/ target/

# echo "Main-Class: main.Main" > manifest.txt

# jar uvf ./target/2dgame-1.0-SNAPSHOT.jar manifest.txt

# cd target
# jar cfvm ../2dgame.jar ../manifest.txt *
# cd ..
# rm manifest.txt

jar --create --file 2dgame.jar --main-class main.Main -C target .

if [ -f 2dgame.jar ]; then
    # java -jar 2dgame.jar
    
    native-image -jar 2dgame.jar
fi


if [ -n "$current_jdk" ]; then
    echo "resetting back to original jdk: $current_jdk"
    sdk use java "$current_jdk"
fi

# java -cp ./target main.Main

