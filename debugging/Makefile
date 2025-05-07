TARGET = bin/hello
SRC = $(wildcard src/*.c)
OBJ = $(patsubst src/%.c, obj/%.o, $(SRC))

SHELL=/bin/bash

run: clean default
	ulimit -c unlimited
	./$(TARGET)
	ulimit -c 0

default: $(TARGET)

clean:
	./dir_prep.sh
	rm -f obj/*.o
	rm -f bin/*

$(TARGET): $(OBJ)
	gcc -o $@ $?

obj/%.o : src/%.c
	# added the -g to include debug symbols
	gcc -g -c $< -o $@ -Iinclude

debug:
	gdb ./$(TARGET)

broken: clean default
	./broken.sh

