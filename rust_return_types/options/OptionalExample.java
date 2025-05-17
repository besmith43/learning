///usr/bin/env jbang "$0" "$@" ; exit $?


import java.util.Optional;

public class OptionalExample {

    public static Optional<String> findByName(String name) {
        if ("John".equals(name)) {
            return Optional.of("John Doe");
        } else {
            return Optional.empty();
        }
    }

    public static void main(String[] args) {
        Optional<String> result = findByName("John");
        if (result.isPresent()) {
            System.out.println(result.get());
        }

        Optional<String> emptyResult = findByName("Jane");
        emptyResult.ifPresent(System.out::println); // Does nothing

        String name = findByName("Jane").orElse("Unknown");
        System.out.println(name);
    }
}
