#!/usr/bin/env bash

set -euo pipefail

readonly BUILD_DIR="target"
readonly JAR_NAME="2dgame.jar"
readonly MANIFEST_FILE="manifest.mf"

rm -rf "${BUILD_DIR}" "${JAR_NAME}" "${MANIFEST_FILE}"
mkdir -p "${BUILD_DIR}"

javac -d "${BUILD_DIR}" $(find src/main/java -name "*.java")

cp -R src/main/resources/. "${BUILD_DIR}/"

printf "Main-Class: main.Main\n" > "${MANIFEST_FILE}"

jar cfm "${JAR_NAME}" "${MANIFEST_FILE}" -C "${BUILD_DIR}" .
rm -f "${MANIFEST_FILE}"

java -jar "${JAR_NAME}"
