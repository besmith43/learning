

# make will run the first task that doesn't start with a . as the default task
# meaning that no parameter was given
# therefore in this file, the default task will run if you simply run "make"
default: clean build deploy

build: build_java build_war

build_java:
	./scripts/build_java.sh

build_war:
	./scripts/build_war.sh

deploy:
	./scripts/deploy.sh

clean:
	./scripts/clean.sh

