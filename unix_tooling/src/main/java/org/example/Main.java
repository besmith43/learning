package org.example;

import java.util.Scanner;
import java.util.ArrayList;
// import java.util.Arrays;
import java.util.List;

public class Main {
    public static void main(String[] args) {
        System.out.printf("Hello and welcome!\n");

        for (int i = 0; i < args.length; i++) {
            System.out.println(args[i]);
        }

        Scanner pipedInput = new Scanner(System.in);

       List<String> pipedInputList = new ArrayList<>();

        String input = pipedInput.nextLine();

        while (pipedInput.hasNext())
        {
            pipedInputList.add(input);
            input = pipedInput.nextLine();
        }

        System.out.println(pipedInputList);
        pipedInputList.clear();
    }
}