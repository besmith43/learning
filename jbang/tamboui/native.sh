#!/usr/bin/env bash

set -euo pipefail

script="HelloWorld.java"
main_class="HelloWorld"
output="HelloWorld"
resource_pattern='dev/tamboui/tui/bindings/.*\.properties'

if ! command -v jbang >/dev/null 2>&1; then
  echo "Error: jbang is not installed or not on PATH." >&2
  exit 1
fi

if ! command -v native-image >/dev/null 2>&1; then
  echo "Error: GraalVM native-image is not installed or not on PATH." >&2
  exit 1
fi

classpath="$(jbang info classpath "$script")"

native-image \
  -cp "$classpath" \
  -o "$output" \
  -H:IncludeResources="$resource_pattern" \
  -H:+SharedArenaSupport \
  --enable-native-access=ALL-UNNAMED \
  "$main_class"
