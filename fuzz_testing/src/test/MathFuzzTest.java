import static org.junit.jupiter.api.Assertions.assertEquals;

import myPackage.Math;

import com.code_intelligence.jazzer.junit.FuzzTest;
import org.junit.jupiter.api.Test;

// see here for more examples
// https://github.com/CodeIntelligenceTesting/jazzer/tree/main/examples/junit/src/test/java/com/example

// docs: https://codeintelligencetesting.github.io/jazzer-docs/jazzer-api/com/code_intelligence/jazzer/api/FuzzedDataProvider.html

// @RunWith(org.junit.platform.runner.JUnitPlatform.class)
class MathFuzzTest {
    @FuzzTest
    void additionFuzzTest(FuzzedDataProvider data) {
        int[] arr = data.ConsumeInts(2);
        assertEquals(arr[0] + arr[1], Math.Add(arr[0], arr[1]));
    }
}
