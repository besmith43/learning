The more you know


We can now develop our script as we would with any Java file.
Once it is ready to be published, we can export it in different formats as follows:

A jar file: jbang export portable helloworld.java. If your script uses dependencies, the next commands are more recommended.
A fatjar: which contains all the dependencies: jbang export fatjar helloworld.java. This method still requires to install a JDK / JRE on the target machine. If you don't want to that, the next commands are more recommended.
A jlink binary that encompases a JDK: jbang export jlink helloworld.java. The binary to run is either helloworld-jlink/bin/helloworld on Unix or helloworld-jlink/bin/helloworld.bat on Windows.
A native imgae: jbang export native helloworld.java. This requires a GraalVM installation.
The script can also be exported as a mavenrepo with: jbang export mavenrepo helloworld.java



### Reference

[the missing scripting tool of the java ecosystem](https://dev.to/worldlinetech/jbang-the-missing-scripting-tool-of-the-java-ecosystem-3f7d)
