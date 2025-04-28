

SHELL=/bin/bash


run: obj
	./hello

obj: compile
	gcc hello.o -o hello

compile: clean
	gcc -c hello.c -o hello.o

clean:
	rm -f hello *.o





