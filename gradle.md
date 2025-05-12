# Gradle

### Recommendations


1. use groovy for the build.gradle

2. 


### how to use it


start a new project

```bash
    gradle init
```


run a project

```bash
    gradle run --args <whatever your arguments are>
```

list all available tasks (sub commands)

```bash
    gradle tasks --all
```

see the order that things get run during build

```bash
    gradle build --console=verbose
```

### make fat jar


all of these go in the ./app/build.gradle


```
    plugins {
        id 'com.github.johnrengelman.shadow' version '7.1.2'
    }
```


```
    jar {
        manifest {
            attributes 'Main-Class': 'your.package.YourMainClass'
        }
    }
```


gradle run command

```bash
    ./gradlew shadowJar
```

run the fat jar

```bash
    java -jar build/libs/[your-project-name]-all.jar
```


### fat jar alternative


add to the jar task the following:


```
    jar {
        manifest {
            attributes 'Main-Class': 'your.package.YourMainClass'
        }

        from {
            configurations.runtimeClasspath.collect { it.isDirectory() ? it : zipTree(it) }
        }
    }
```
