package com.example.versioning;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;

import org.junit.jupiter.api.Test;

class SemanticVersionTest {
    @Test
    void parsesValidVersion() {
        SemanticVersion version = SemanticVersion.parse("1.2.3");

        assertEquals(1, version.major());
        assertEquals(2, version.minor());
        assertEquals(3, version.patch());
    }

    @Test
    void rejectsInvalidVersion() {
        assertThrows(IllegalArgumentException.class, () -> SemanticVersion.parse("1.2"));
        assertThrows(IllegalArgumentException.class, () -> SemanticVersion.parse("1.2.3-beta"));
        assertThrows(IllegalArgumentException.class, () -> SemanticVersion.parse("01.2.3"));
    }

    @Test
    void bumpsPatch() {
        assertEquals("1.2.4", SemanticVersion.parse("1.2.3").bumpPatch().toString());
    }

    @Test
    void bumpsMinorAndResetsPatch() {
        assertEquals("1.3.0", SemanticVersion.parse("1.2.3").bumpMinor().toString());
    }

    @Test
    void bumpsMajorAndResetsMinorAndPatch() {
        assertEquals("2.0.0", SemanticVersion.parse("1.2.3").bumpMajor().toString());
    }
}
