package org.example;

//TIP To <b>Run</b> code, press <shortcut actionId="Run"/> or
// click the <icon src="AllIcons.Actions.Execute"/> icon in the gutter.
public class Main {
    public static void main(String[] args) {
        //TIP Press <shortcut actionId="ShowIntentionActions"/> with your caret at the highlighted text
        // to see how IntelliJ IDEA suggests fixing it.
        System.out.printf("Hello and welcome!");

        for (int i = 1; i <= 5; i++) {
            //TIP Press <shortcut actionId="Debug"/> to start debugging your code. We have set one <icon src="AllIcons.Debugger.Db_set_breakpoint"/> breakpoint
            // for you, but you can always add more by pressing <shortcut actionId="ToggleLineBreakpoint"/>.
            System.out.println("i = " + i);
        }

        int num = 4;

        try {
            IsOdd(num);
            System.out.println("num is odd");
        } catch (AssertionError e) {
            System.out.println("num is even");
            System.out.println("Error Message: " + e.getMessage());
        }

        System.out.println("made it past the assert");

    }

    public static void IsOdd(int num) throws AssertionError {
        // assert does nothing unless the vm parameter -ea or -enableassertions is passed at runtime

        // assert with colon (:)
        // assert expression : assertionError message;
        assert num % 2 == 1 : "num value " + num + " is not odd";

        // just assert and throw a null message
        assert num % 2 == 1;
    }
}