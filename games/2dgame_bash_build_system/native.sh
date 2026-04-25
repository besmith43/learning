#!/usr/bin/env bash -i

set -euo pipefail

readonly BUILD_DIR="target"
readonly JAR_NAME="2dgame.jar"
readonly MANIFEST_FILE="manifest.mf"
readonly NATIVE_TMP_DIR="${BUILD_DIR}/native-tmp"
readonly NATIVE_CONFIG_DIR="${BUILD_DIR}/native-image-config"
readonly NATIVE_REPORTS_DIR="${BUILD_DIR}/native-reports"
readonly IMAGE_NAME="2dgame"

current_jdk=""

ensure_native_image() {
    if [ -z "$(which native-image)" ] && [ "$(type -t sdk)" = "function" ]; then
        current_jdk="$(sdk list java | grep -i ">>>" | awk '{ print $(NF) }')"

        if [ -n "${current_jdk}" ]; then
            echo "found current jdk: ${current_jdk}"
        fi

        graal_installed_version="$(sdk list java | grep -i graalce | grep -i installed | head -n 1 | awk '{ print $(NF) }')"

        if [ -n "${graal_installed_version}" ]; then
            echo "graalvm installed version found ${graal_installed_version}"
            sdk use java "${graal_installed_version}"
        else
            graal_latest_version="$(sdk list java | grep -i graalce | head -n 1 | awk '{ print $(NF) }')"

            if [ -n "${graal_latest_version}" ]; then
                echo "graalvm latest version found ${graal_latest_version}"
                sdk install java "${graal_latest_version}"
            fi
        fi
    elif [ -z "$(which native-image)" ]; then
        echo "you need to be using graalvm"
        exit 1
    fi
}

cleanup_jdk() {
    if [ -n "${current_jdk}" ]; then
        echo "resetting back to original jdk: ${current_jdk}"
        sdk use java "${current_jdk}"
    fi
}

build_jar() {
    rm -rf "${BUILD_DIR}" "${JAR_NAME}" "${MANIFEST_FILE}"
    mkdir -p "${BUILD_DIR}"

    javac -d "${BUILD_DIR}" $(find src/main/java -name "*.java")
    cp -R src/main/resources/. "${BUILD_DIR}/"

    printf "Main-Class: main.Main\n" > "${MANIFEST_FILE}"
    jar cfm "${JAR_NAME}" "${MANIFEST_FILE}" -C "${BUILD_DIR}" .
    rm -f "${MANIFEST_FILE}"
}

run_agent() {
    mkdir -p "${NATIVE_CONFIG_DIR}"

    echo "launching the jar with the native-image tracing agent"
    echo "exercise the game, then close it to write metadata into ${NATIVE_CONFIG_DIR}"

    java \
        -agentlib:native-image-agent=config-output-dir="${NATIVE_CONFIG_DIR}" \
        -jar "${JAR_NAME}"
}

build_native_image() {
    mkdir -p "${NATIVE_TMP_DIR}" "${NATIVE_REPORTS_DIR}"

    config_args=()
    if [ -d "${NATIVE_CONFIG_DIR}" ] && [ -n "$(rg --files "${NATIVE_CONFIG_DIR}" 2>/dev/null)" ]; then
        echo "using native-image config from ${NATIVE_CONFIG_DIR}"
        config_args+=("-H:ConfigurationFileDirectories=${NATIVE_CONFIG_DIR}")
    else
        echo "warning: ${NATIVE_CONFIG_DIR} is empty"
        echo "warning: Swing/AWT native images often need tracing-agent metadata before they run correctly"
        echo "warning: run ./native.sh --agent first, interact with the app, close it, then rerun ./native.sh"
    fi

    native-image \
        -H:+UnlockExperimentalVMOptions \
        -H:TempDirectory="${NATIVE_TMP_DIR}" \
        --diagnostics-mode \
        --no-fallback \
        -Djava.awt.headless=false \
        -H:Path="${BUILD_DIR}" \
        -H:Name="${IMAGE_NAME}" \
        "${config_args[@]}" \
        -jar "${JAR_NAME}"

    echo "built native executable at ${BUILD_DIR}/${IMAGE_NAME}"
    echo "run with: ./${BUILD_DIR}/${IMAGE_NAME}"
}

main() {
    ensure_native_image
    trap cleanup_jdk EXIT

    build_jar

    if [ "${1:-}" = "--agent" ]; then
        run_agent
        return
    fi

    build_native_image
}

main "${@}"
