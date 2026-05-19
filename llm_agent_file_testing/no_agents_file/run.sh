#!/usr/bin/env bash
set -euo pipefail

./build.sh
docker compose up --build
