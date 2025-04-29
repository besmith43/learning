#!/usr/bin/env bash


if [ ! -f bad_input.txt ]; then
    echo 2222 > bad_input.txt
fi

if [ "$(ulimit -c)" != "unlimited" ]; then
    ulimit -c unlimited
fi

echo "./core" | sudo tee /proc/sys/kernel/core_pattern

./bin/hello < bad_input.txt

echo "|/usr/share/apport/apport -p%p -s%s -c%c -d%d -P%P -u%u -g%g -- %E" | sudo tee /proc/sys/kernel/core_pattern

gdb ./bin/hello ./core

if [ -f ./core ]; then
    rm ./core
fi

if [ -f ./bad_input.txt ]; then
    rm ./bad_input.txt
fi

cat /proc/sys/kernel/core_pattern
