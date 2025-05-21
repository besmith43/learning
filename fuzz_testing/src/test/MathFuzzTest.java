import static org.junit.jupiter.api.Assertions.assertEquals;

import myPackage.Math;

import com.code_intelligence.jazzer.junit.FuzzTest;
import org.junit.jupiter.api.Test;

// see here for more examples
// https://github.com/CodeIntelligenceTesting/jazzer/tree/main/examples/junit/src/test/java/com/example

// @RunWith(org.junit.platform.runner.JUnitPlatform.class)
class MathFuzzTest {
    @FuzzTest
    void additionFuzzTest(int input1, int input2) {
        assertEquals(input1 + input2, Math.Add(input1, input2));
    }
}
