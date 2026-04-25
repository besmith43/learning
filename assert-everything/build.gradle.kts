plugins {
    application
    java
}

group = "org.example"
version = "1.0-SNAPSHOT"

repositories {
    mavenCentral()
}

application {
    mainClass.set("org.example.Main")
    applicationDefaultJvmArgs = listOf("-ea")
}

tasks.withType<JavaCompile>().configureEach {
    options.release.set(23)
    options.encoding = "UTF-8"
}

tasks.named<JavaExec>("run") {
    jvmArgs("-ea")
}

tasks.withType<Test>().configureEach {
    jvmArgs("-ea")
}
