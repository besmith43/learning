#!/usr/bin/env bash
set -euo pipefail

./build.sh
export GRADLE_USER_HOME="${GRADLE_USER_HOME:-"$PWD/.gradle-home"}"
./gradlew --no-daemon bootWar
