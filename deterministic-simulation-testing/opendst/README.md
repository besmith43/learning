# OpenDST Order Workflow Example

This is a compact distributed Java microservice example for comparing standard unit tests, Playwright API e2e tests, jqwik fuzz/property tests, and OpenDST deterministic simulation testing.

## Requirements

- Java 25+
- Maven 3.9+
- Node.js 20+ for Playwright e2e tests

## Test Layout

- `tests/unit` - standard JUnit tests for pure workflow logic.
- `tests/fuzz` - jqwik property tests for generated order scenarios.
- `tests/e2e` - Playwright API tests against live HTTP services.
- `tests/dst` - OpenDST service mains and the source copy of the deployment topology.
- `deployment.yaml` - root OpenDST descriptor consumed by the Maven plugin.

## Commands

```bash
./scripts/build.sh
./scripts/test-unit.sh
./scripts/test-fuzz.sh
./scripts/test-java.sh
./scripts/test-e2e.sh
./scripts/build-opendst.sh
./scripts/run-opendst.sh
./scripts/run-local.sh
```

The scripts use a workspace-local Maven repository at `.m2/repository`. `build-opendst.sh` and `run-opendst.sh` also pin Java to the Homebrew Java 25 installation when it exists; set `OPENDST_JAVA_HOME` to override that path.
