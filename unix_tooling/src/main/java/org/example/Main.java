package org.example;

import java.util.Scanner;
import java.util.ArrayList;
// import java.util.Arrays;
import java.util.List;

public class Main {
    public static void main(String[] args) {
        System.out.printf("Hello and welcome!\n");

        for (int i = 0; i < args.length; i++) {
            System.out.println("Arg: " + args[i]);
        }

        System.out.println("done with args");

        Scanner pipedInput = new Scanner(System.in);

       List<String> pipedInputList = new ArrayList<>();

        String input;

        System.out.println("Starting while loop input scan");

        if (pipedInput.hasNext())
        {
            System.out.println("piped input has something");

            System.out.println("Starting the while loop");

            while (pipedInput.hasNext())
            {
                System.out.println("input has next");
                input = pipedInput.nextLine();
                pipedInputList.add(input);
            }

            System.out.println("Piped Input: " + pipedInputList);
            pipedInputList.clear();
        }

    }
}