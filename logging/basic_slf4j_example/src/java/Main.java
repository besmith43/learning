import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class Main {
    private static final Logger logger = LoggerFactory.getLogger(Main.class);

    public static void main(String[] args) {
        logger.info("This is an info message.");
        logger.debug("This is a debug message.");
        logger.warn("this is a warning!");
        logger.error("This is an error message.");
    }
}

