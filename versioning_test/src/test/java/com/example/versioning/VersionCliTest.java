package com.example.versioning;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.PrintStream;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.io.TempDir;

class VersionCliTest {
    @TempDir
    Path tempDir;

    @Test
    void printsCurrentVersion() throws IOException {
        CliFixture fixture = fixtureWithVersion("1.2.3");

        int exitCode = fixture.run("current");

        assertEquals(0, exitCode);
        assertEquals("1.2.3" + System.lineSeparator(), fixture.stdout());
    }

    @Test
    void bumpsPatchAndWritesVersionFile() throws IOException {
        CliFixture fixture = fixtureWithVersion("1.2.3");

        int exitCode = fixture.run("bump", "patch");

        assertEquals(0, exitCode);
        assertEquals("1.2.3 -> 1.2.4" + System.lineSeparator(), fixture.stdout());
        assertEquals("1.2.4" + System.lineSeparator(), Files.readString(fixture.versionFile()));
    }

    @Test
    void setsExplicitVersion() throws IOException {
        CliFixture fixture = fixtureWithVersion("1.2.3");

        int exitCode = fixture.run("set", "2.0.0");

        assertEquals(0, exitCode);
        assertEquals("1.2.3 -> 2.0.0" + System.lineSeparator(), fixture.stdout());
        assertEquals("2.0.0" + System.lineSeparator(), Files.readString(fixture.versionFile()));
    }

    @Test
    void validatesVersionWithoutWritingFile() throws IOException {
        CliFixture fixture = fixtureWithVersion("1.2.3");

        int exitCode = fixture.run("validate", "2.0.0");

        assertEquals(0, exitCode);
        assertEquals("2.0.0 is valid" + System.lineSeparator(), fixture.stdout());
        assertEquals("1.2.3" + System.lineSeparator(), Files.readString(fixture.versionFile()));
    }

    @Test
    void returnsNonZeroForInvalidCommand() throws IOException {
        CliFixture fixture = fixtureWithVersion("1.2.3");

        int exitCode = fixture.run("bump", "banana");

        assertEquals(1, exitCode);
        assertEquals("", fixture.stdout());
        assertTrue(fixture.stderr().contains("Unknown bump type: banana"));
    }

    private CliFixture fixtureWithVersion(String version) throws IOException {
        Path versionFile = tempDir.resolve("VERSION");
        Files.writeString(versionFile, version + System.lineSeparator());
        return new CliFixture(versionFile);
    }

    private static final class CliFixture {
        private final Path versionFile;
        private final ByteArrayOutputStream stdout = new ByteArrayOutputStream();
        private final ByteArrayOutputStream stderr = new ByteArrayOutputStream();
        private final VersionCli cli;

        private CliFixture(Path versionFile) {
            this.versionFile = versionFile;
            this.cli = new VersionCli(
                    new VersionStore(versionFile),
                    new PrintStream(stdout, true, StandardCharsets.UTF_8),
                    new PrintStream(stderr, true, StandardCharsets.UTF_8));
        }

        private int run(String... args) {
            return cli.run(args);
        }

        private Path versionFile() {
            return versionFile;
        }

        private String stdout() {
            return stdout.toString(StandardCharsets.UTF_8);
        }

        private String stderr() {
            return stderr.toString(StandardCharsets.UTF_8);
        }
    }
}
