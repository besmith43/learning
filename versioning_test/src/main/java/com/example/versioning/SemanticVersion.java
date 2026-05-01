package com.example.versioning;

import java.util.Objects;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public record SemanticVersion(int major, int minor, int patch) {
    private static final Pattern VERSION_PATTERN = Pattern.compile("(0|[1-9]\\d*)\\.(0|[1-9]\\d*)\\.(0|[1-9]\\d*)");

    public SemanticVersion {
        if (major < 0 || minor < 0 || patch < 0) {
            throw new IllegalArgumentException("Version parts must be non-negative");
        }
    }

    public static SemanticVersion parse(String input) {
        Objects.requireNonNull(input, "input");
        Matcher matcher = VERSION_PATTERN.matcher(input.trim());
        if (!matcher.matches()) {
            throw new IllegalArgumentException("Invalid semantic version: " + input);
        }

        try {
            return new SemanticVersion(
                    Integer.parseInt(matcher.group(1)),
                    Integer.parseInt(matcher.group(2)),
                    Integer.parseInt(matcher.group(3)));
        } catch (NumberFormatException exception) {
            throw new IllegalArgumentException("Version part is too large: " + input, exception);
        }
    }

    public SemanticVersion bumpPatch() {
        return new SemanticVersion(major, minor, Math.addExact(patch, 1));
    }

    public SemanticVersion bumpMinor() {
        return new SemanticVersion(major, Math.addExact(minor, 1), 0);
    }

    public SemanticVersion bumpMajor() {
        return new SemanticVersion(Math.addExact(major, 1), 0, 0);
    }

    @Override
    public String toString() {
        return major + "." + minor + "." + patch;
    }
}
