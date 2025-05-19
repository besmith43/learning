import java.util.logging.Logger;
import java.util.logging.LogManager;
import java.util.logging.Level;

// code came from here: https://www.digitalocean.com/community/tutorials/logger-in-java-logging-example

public class Main {
    public static void main(String[] args) {
        Logger logger = Logger.getLogger(Main.class.getName());
        logger.setLevel(Level.FINE);
        // LogManager.getLogManager().readConfiguration(new FileInputStream("mylogging.properties"));

        logger.log(Level.INFO, "This is an info message.");
        logger.log(Level.FINE, "This is a debug message.");
        logger.log(Level.WARNING, "this is a warning!");
        logger.log(Level.SEVERE, "This is an error message.");
    }
}

