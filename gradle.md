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
    gradle run
```

list all available tasks (sub commands)

```bash
    gradle tasks --all
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

