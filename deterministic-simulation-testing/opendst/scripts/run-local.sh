#!/usr/bin/env bash
set -euo pipefail

source "$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)/common.sh"

BASE_PORT="${1:-18080}"

mvn_local -DskipTests compile org.codehaus.mojo:exec-maven-plugin:3.6.2:java \
  -Dexec.mainClass=com.example.opendst.orders.http.LocalClusterMain \
  -Dexec.args="${BASE_PORT}"
