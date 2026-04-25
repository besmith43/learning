#!/usr/bin/env bash
set -euo pipefail

source "$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)/common.sh"

if [[ ! -d node_modules ]]; then
  npm install
fi

npm run test:e2e
