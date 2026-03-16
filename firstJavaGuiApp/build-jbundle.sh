#!/usr/bin/env bash


if [ ! -f "$(which jbundle)" ]; then
    curl -sSL https://raw.githubusercontent.com/avelino/jbundle/main/install.sh | sh
fi

jbundle build -i GUI.jar -o jbundle_GUI

