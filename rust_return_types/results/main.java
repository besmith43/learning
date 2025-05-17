///usr/bin/env jbang "$0" "$@" ; exit $?


import static java.lang.System.*;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

public class main {

    public static void main(String... args) {
        var check1 = getFileContents("testfile.txt");

        if (check1.isSuccess()) {
            System.out.println("Success: " + check1.getValue());
        } else {
            System.out.println("Failure: " + check1.getError().getMessage());
        }

        var check2 = getFileContents("nonexsistent.txt");

        if (check2.isSuccess()) {
            System.out.println("Success: " + check2.getValue());
        } else {
            System.out.println("Failure: " + check2.getError().getMessage());
        }
    }

    public static Result<String, IOException> getFileContents(String checkFilePath) {
        try {
            Path filePath = Paths.get(checkFilePath);
            String content = Files.readString(filePath);

            return Result.success(content);
        } catch (IOException e) {
            return Result.failure(e);
        }
    }
}
