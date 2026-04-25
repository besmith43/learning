@echo off
set DIR=%~dp0
set CLASSPATH=%DIR%gradle\wrapper\gradle-wrapper-main.jar;%DIR%gradle\wrapper\gradle-wrapper-shared.jar;%DIR%gradle\wrapper\gradle-cli.jar

java %JAVA_OPTS% %GRADLE_OPTS% -classpath "%CLASSPATH%" org.gradle.wrapper.GradleWrapperMain %*
