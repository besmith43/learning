///usr/bin/env jbang "$0" "$@" ; exit $?


import static java.lang.System.*;
import javax.swing.*;

public class hello {

    public static void main(String... args) {
        // Create a new JFrame (window)
        JFrame frame = new JFrame("Hello, World!");

        // Create a JLabel with the text "Hello, World!"
        JLabel label = new JLabel("Hello, World!");

        // Add the label to the frame's content pane
        frame.getContentPane().add(label);

        // Set the frame's size
        frame.setSize(300, 200);

        // Make the frame visible
        frame.setVisible(true);

        // Set the default close operation (exit when the window is closed)
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }
}
