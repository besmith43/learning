# Business Expenditures Chart with Java and R

This example is a small Java desktop app that loads a CSV of business expenditures and uses R through the `rJava` package's JRI runtime to generate a category bar chart.

## CSV Schema

The app expects these headers:

```text
date,category,amount,vendor
```

Rules:
- `date` must be ISO-8601, for example `2026-01-03`
- `amount` must be numeric and non-negative
- blank required fields are rejected

Sample data is in [sample-data/expenses.csv](/Users/besmith/Developer/Personal/learning-java/using_r_in_java/sample-data/expenses.csv).

## Local Setup

1. Install JDK 21 and point `JAVA_HOME` at it when building or running the app.
2. Install R for macOS.
3. In R, install `rJava`:

```r
install.packages("rJava")
```

4. Set either `JRI_HOME` directly, or set `R_HOME` and let the app search common `rJava` locations.

Typical paths:

```bash
export JAVA_HOME=$(/usr/libexec/java_home -v 21)
export R_HOME=/Library/Frameworks/R.framework/Resources
export JRI_HOME="$R_HOME/library/rJava/jri"
```

If `rJava` is installed under `site-library`, use:

```bash
export JRI_HOME="$R_HOME/site-library/rJava/jri"
```

If R was installed before the JDK, you may also need:

```bash
R CMD javareconf
```

## Run

Generate the Gradle wrapper if it is not already present:

```bash
gradle wrapper
```

Then launch the app:

```bash
./gradlew run
```

Choose a CSV and the app will:
- validate the file
- pass category and amount vectors into R through JRI
- ask R to aggregate totals by category
- render a PNG bar chart
- display that chart in the Swing window

## Project Layout

- `src/main/java/com/example/expenses`: Swing app, CSV parsing, and JRI bridge
- `sample-data/expenses.csv`: starter input file
- `build.gradle.kts`: Gradle Kotlin DSL configuration
