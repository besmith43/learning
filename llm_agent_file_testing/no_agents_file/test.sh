#!/usr/bin/env bash
set -euo pipefail

export GRADLE_USER_HOME="${GRADLE_USER_HOME:-"$PWD/.gradle-home"}"
./gradlew --no-daemon frontendTest test integrationTest e2eTest
