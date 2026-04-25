#!/usr/bin/env bash
set -euo pipefail

source "$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)/common.sh"

use_opendst_java

if [[ ! -f target/order-workflow-1.0.0-SNAPSHOT-opendst.jar ]]; then
  "${SCRIPT_DIR}/build-opendst.sh"
fi

java -jar target/order-workflow-1.0.0-SNAPSHOT-opendst.jar \
  --working-dir target/opendst-work \
  --stagnation-limit "${OPENDST_STAGNATION_LIMIT:-5}" \
  --duration "${OPENDST_DURATION:-1000}" \
  --fork-count "${OPENDST_FORK_COUNT:-1}" \
  --stop "${OPENDST_STOP:-all-pass}"
