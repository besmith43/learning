#!/usr/bin/env bash


function quit() {
    read -n 1 -s -r -p "Would you like to quit? (y/n)" key
    if [ "$key" == "y" ]; then
        exit
    fi

    echo "Booh!"
}


trap quit SIGINT SIGTERM

echo "pid is $$"
echo "this program will do nothing but run forever"
echo "however if you try to close it with ctrl + c"
echo "all that will happen is you'll get \"Booh!\""
echo "printed to the screen"

while :
do
    sleep 60
done



