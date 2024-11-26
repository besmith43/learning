# understanding kotlin as a scripting language


Look at the run.sh script on how to call this script like you'd expect
However the kotlin requires the .main.kts extension in order to run.
I don't know if there's a way around this.


To test this, let's do the following:

1) copy the hello.main.kts to hello

```bash
	cp hello.main.kts hello
```

2) run the new script

```bash
	./hello
```

3) read the error

```
	error: could not find or load main class ./hello
```

