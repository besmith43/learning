# Setup

1) add the following to the $CATALINA_BASE/conf/content.xml:

```
	<Resource name="jdbc/person" auth="Container"
		type="javax.sql.DataSource"
		driverClassName="org.sqlite.JDBC"
        url="jdbc:sqlite:${catalina.base}/db/person.sqlite"
	/>
```

2) add the jar file to the $CATALINA_BASE/lib/:


```bash
    wget https://repo1.maven.org/maven2/org/xerial/sqlite-jdbc/3.49.1.0/sqlite-jdbc-3.49.1.0.jar
    mv sqlite-jdbc-3.49.1.0.jar $CATALINA_BASE/lib/
```

3) use the index.jsp as an example of using the jdbc in the context.xml:

```bash
    cp index.jsp $CATALINA_BASE/webapps/ROOT/
```

4) make the db folder and copy the person.sqlite into the it:

```bash
    mkdir $CATALINA_BASE/db
    cp person.sqlite $CATALINA_BASE/db
```

