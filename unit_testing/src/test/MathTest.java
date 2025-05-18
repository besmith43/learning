import static org.junit.jupiter.api.Assertions.assertEquals;

import myPackage.Math;

import org.junit.jupiter.api.Test;

// @RunWith(org.junit.platform.runner.JUnitPlatform.class)
class MathTest {
    @Test
    void addition() {
        assertEquals(7, Math.Add(5, 2));
    }
}
