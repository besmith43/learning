import java.lang.System.Logger;
import java.lang.System.Logger.Level;

public class Main {
    private static final Logger logger = System.getLogger("MyLogger");

    public static void main(String[] args) {
        logger.log(Level.INFO, "Application started");
        try {
            int result = 10 / 0;
        } catch (ArithmeticException e) {
            logger.log(Level.ERROR, "An error occurred", e);
        }
    }
}

