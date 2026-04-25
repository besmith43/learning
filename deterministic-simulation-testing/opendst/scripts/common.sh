#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd -- "${SCRIPT_DIR}/.." && pwd)"
MAVEN_REPO="${REPO_ROOT}/.m2/repository"

cd "${REPO_ROOT}"

mvn_local() {
  mvn -Dmaven.repo.local="${MAVEN_REPO}" "$@"
}

use_opendst_java() {
  if [[ -n "${OPENDST_JAVA_HOME:-}" ]]; then
    export JAVA_HOME="${OPENDST_JAVA_HOME}"
  elif [[ -d "/opt/homebrew/Cellar/openjdk/25.0.2/libexec/openjdk.jdk/Contents/Home" ]]; then
    export JAVA_HOME="/opt/homebrew/Cellar/openjdk/25.0.2/libexec/openjdk.jdk/Contents/Home"
  fi

  if [[ -n "${JAVA_HOME:-}" ]]; then
    export PATH="${JAVA_HOME}/bin:${PATH}"
  fi
}
