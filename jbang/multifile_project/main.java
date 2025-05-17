///usr/bin/env jbang "$0" "$@" ; exit $?

import static java.lang.System.*;
import model.Person;

public class main {

    public static void main(String... args) {
        if (args.length != 0) {
            Person p = new Person(args[0]);
            System.out.println("Hello " + p.getName());
        } else {
            System.out.println("you need to pass in a name");
        }
    }
}
