# Gradle Actions

Use the Gradle wrapper from the project root:

```bash
./gradlew <task>
```

On Windows, use `gradlew.bat` instead of `./gradlew`.

## Run Tests

```bash
./gradlew test
```

## Build the Project

```bash
./gradlew build
```

This compiles the application, runs tests, and creates build outputs.

## Create a JAR

```bash
./gradlew jar
```

The JAR is written to:

```text
build/libs/versioning-test-0.1.0.jar
```

Note: this JAR is not currently configured with a `Main-Class`, so it is not directly runnable with `java -jar`.

## Run the Application

The application is a versioning CLI. Run it through Gradle with:

```bash
./gradlew run --args="current"
```

Common commands:

```bash
./gradlew run --args="current"
./gradlew run --args="validate 1.2.3"
./gradlew run --args="set 1.2.3"
./gradlew run --args="bump patch"
./gradlew run --args="bump minor"
./gradlew run --args="bump major"
```

## Bump the Version Number

Patch version:

```bash
./gradlew run --args="bump patch"
```

Minor version:

```bash
./gradlew run --args="bump minor"
```

Major version:

```bash
./gradlew run --args="bump major"
```

These commands update the `VERSION` file used by the application.

Important: the Gradle artifact version is currently set separately in `build.gradle.kts`:

```kotlin
version = "0.1.0"
```

That means bumping the app version in `VERSION` does not automatically change the JAR filename.
