plugins {
    java
    war
    id("org.springframework.boot") version "4.0.6"
    id("io.spring.dependency-management") version "1.1.7"
}

group = "com.example"
version = "0.1.0"

java {
    toolchain {
        languageVersion = JavaLanguageVersion.of(25)
    }
}

configurations {
    compileOnly {
        extendsFrom(configurations.annotationProcessor.get())
    }
}

dependencies {
    implementation("org.springframework.boot:spring-boot-starter-data-jpa")
    implementation("org.springframework.boot:spring-boot-starter-security")
    implementation("org.springframework.boot:spring-boot-starter-validation")
    implementation("org.springframework.boot:spring-boot-starter-web")
    implementation("com.fasterxml.jackson.core:jackson-databind")
    implementation("org.postgresql:postgresql")
    runtimeOnly("com.h2database:h2")

    providedRuntime("org.springframework.boot:spring-boot-starter-tomcat-runtime")

    testImplementation("org.springframework.boot:spring-boot-starter-test")
    testImplementation("org.springframework.boot:spring-boot-test-autoconfigure")
    testImplementation("org.springframework.security:spring-security-test")
    testImplementation("org.testcontainers:junit-jupiter:1.21.3")
    testImplementation("org.testcontainers:postgresql:1.21.3")
    testImplementation("com.microsoft.playwright:playwright:1.52.0")
    testRuntimeOnly("org.junit.platform:junit-platform-launcher")
}

tasks.withType<Test> {
    useJUnitPlatform()
}

val integrationTest by sourceSets.creating {
    compileClasspath += sourceSets.main.get().output + configurations.testRuntimeClasspath.get()
    runtimeClasspath += output + compileClasspath
}

configurations[integrationTest.implementationConfigurationName].extendsFrom(configurations.testImplementation.get())
configurations[integrationTest.runtimeOnlyConfigurationName].extendsFrom(configurations.testRuntimeOnly.get())

tasks.register<Test>("integrationTest") {
    description = "Runs integration tests."
    group = "verification"
    testClassesDirs = integrationTest.output.classesDirs
    classpath = integrationTest.runtimeClasspath
    shouldRunAfter(tasks.test)
    useJUnitPlatform()
}

val e2eTest by sourceSets.creating {
    compileClasspath += sourceSets.main.get().output + configurations.testRuntimeClasspath.get()
    runtimeClasspath += output + compileClasspath
}

configurations[e2eTest.implementationConfigurationName].extendsFrom(configurations.testImplementation.get())
configurations[e2eTest.runtimeOnlyConfigurationName].extendsFrom(configurations.testRuntimeOnly.get())

tasks.register<Test>("e2eTest") {
    description = "Runs Playwright end-to-end tests."
    group = "verification"
    testClassesDirs = e2eTest.output.classesDirs
    classpath = e2eTest.runtimeClasspath
    shouldRunAfter("integrationTest")
    useJUnitPlatform()
}

tasks.register<Exec>("frontendInstall") {
    workingDir = file("frontend")
    commandLine("npm", "install")
    inputs.file("frontend/package.json")
    outputs.dir("frontend/node_modules")
}

tasks.register<Exec>("frontendTest") {
    workingDir = file("frontend")
    commandLine("npm", "test", "--", "--run")
    dependsOn("frontendInstall")
}

tasks.register<Exec>("frontendBuild") {
    workingDir = file("frontend")
    commandLine("npm", "run", "build")
    dependsOn("frontendInstall")
    inputs.dir("frontend/src")
    inputs.file("frontend/index.html")
    inputs.file("frontend/package.json")
    outputs.dir("frontend/dist")
}

tasks.named<ProcessResources>("processResources") {
    dependsOn("frontendBuild")
    from(layout.projectDirectory.dir("frontend/dist")) {
        into("static")
    }
}

tasks.named("check") {
    dependsOn("frontendTest", "integrationTest", "e2eTest")
}
