#!/usr/bin/env bash


mvn compiler:compile

if [ $? -ne 0 ]; then
	echo compile failed
	exit 1
fi

/opt/homebrew/Cellar/openjdk/23/libexec/openjdk.jdk/Contents/Home/bin/java -javaagent:/Users/besmith/Applications/IntelliJ\ IDEA\ Community\ Edition.app/Contents/lib/idea_rt.jar=51482:/Users/besmith/Applications/IntelliJ\ IDEA\ Community\ Edition.app/Contents/bin -Dfile.encoding=UTF-8 -Dsun.stdout.encoding=UTF-8 -Dsun.stderr.encoding=UTF-8 -classpath /Users/besmith/Developer/Personal/learning-java/unix_tooling/target/classes org.example.Main $(cat input.txt)
