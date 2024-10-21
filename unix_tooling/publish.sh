#!/usr/bin/env bash


mvn package

# for anything else
echo "#!/usr/bin/env java -jar" > app.jar

# for synology
# echo "#!java -jar" > app.jar


echo "" >> app.jar
cat target/unix_tooling-1.0-SNAPSHOT.jar >> app.jar

chmod +x app.jar


cat input.txt | ./app.jar $(cat input.txt)
