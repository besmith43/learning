package com.example.versioning;

import java.io.IOException;
import java.io.PrintStream;
import java.nio.file.Path;
import java.util.Arrays;

public final class VersionCli {
    private static final String USAGE = """
            Usage:
              current
              bump patch|minor|major
              set <major.minor.patch>
              validate <major.minor.patch>
              help
            """;

    private final VersionStore versionStore;
    private final PrintStream out;
    private final PrintStream err;

    public VersionCli(VersionStore versionStore, PrintStream out, PrintStream err) {
        this.versionStore = versionStore;
        this.out = out;
        this.err = err;
    }

    public static void main(String[] args) {
        VersionStore store = new VersionStore(Path.of("VERSION"));
        int exitCode = new VersionCli(store, System.out, System.err).run(args);
        if (exitCode != 0) {
            System.exit(exitCode);
        }
    }

    int run(String[] args) {
        if (args.length == 0 || "help".equals(args[0])) {
            out.print(USAGE);
            return 0;
        }

        try {
            return switch (args[0]) {
                case "current" -> current(args);
                case "bump" -> bump(args);
                case "set" -> set(args);
                case "validate" -> validate(args);
                default -> fail("Unknown command: " + args[0]);
            };
        } catch (IllegalArgumentException | IOException | ArithmeticException exception) {
            return fail(exception.getMessage());
        }
    }

    private int current(String[] args) throws IOException {
        requireArgCount(args, 1);
        out.println(versionStore.read());
        return 0;
    }

    private int bump(String[] args) throws IOException {
        requireArgCount(args, 2);

        SemanticVersion current = versionStore.read();
        SemanticVersion next = switch (args[1]) {
            case "patch" -> current.bumpPatch();
            case "minor" -> current.bumpMinor();
            case "major" -> current.bumpMajor();
            default -> throw new IllegalArgumentException("Unknown bump type: " + args[1]);
        };

        versionStore.write(next);
        out.println(current + " -> " + next);
        return 0;
    }

    private int set(String[] args) throws IOException {
        requireArgCount(args, 2);

        SemanticVersion next = SemanticVersion.parse(args[1]);
        SemanticVersion current = versionStore.read();
        versionStore.write(next);
        out.println(current + " -> " + next);
        return 0;
    }

    private int validate(String[] args) {
        requireArgCount(args, 2);

        SemanticVersion version = SemanticVersion.parse(args[1]);
        out.println(version + " is valid");
        return 0;
    }

    private void requireArgCount(String[] args, int expected) {
        if (args.length != expected) {
            throw new IllegalArgumentException("Expected " + expected + " argument(s), got "
                    + args.length + ": " + Arrays.toString(args));
        }
    }

    private int fail(String message) {
        err.println("Error: " + message);
        err.print(USAGE);
        return 1;
    }
}
