package com.example.versioning;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

public final class VersionStore {
    private final Path versionFile;

    public VersionStore(Path versionFile) {
        this.versionFile = versionFile;
    }

    public SemanticVersion read() throws IOException {
        if (!Files.exists(versionFile)) {
            throw new IOException("Version file does not exist: " + versionFile.toAbsolutePath());
        }

        return SemanticVersion.parse(Files.readString(versionFile));
    }

    public void write(SemanticVersion version) throws IOException {
        Files.writeString(versionFile, version + System.lineSeparator());
    }
}
