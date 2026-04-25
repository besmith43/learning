plugins {
    application
}

group = "com.example.expenses"
version = "1.0.0"

repositories {
    mavenCentral()
}

java {
    toolchain {
        languageVersion.set(JavaLanguageVersion.of(21))
    }
}

application {
    mainClass.set("com.example.expenses.ExpenseChartApplication")
}

tasks.withType<JavaCompile>().configureEach {
    options.encoding = "UTF-8"
    options.release.set(21)
}

tasks.withType<JavaExec>().configureEach {
    val rHome = System.getenv("R_HOME")
    val jriHome = System.getenv("JRI_HOME")
    if (!jriHome.isNullOrBlank()) {
        classpath = classpath.plus(files("$jriHome/JRI.jar"))
    } else if (!rHome.isNullOrBlank()) {
        classpath = classpath.plus(
            files(
                "$rHome/library/rJava/jri/JRI.jar",
                "$rHome/site-library/rJava/jri/JRI.jar"
            )
        )
    }
}

tasks.register<Copy>("installSampleData") {
    from(layout.projectDirectory.dir("sample-data"))
    into(layout.buildDirectory.dir("sample-data"))
}
