package com.example.besmith;

import java.util.Scanner;

/**
 * Hello world!
 **/

public class App 
{
    public static void main( String[] args )
    {
        System.out.println( "Hello World!" );

        Scanner scanner = new Scanner(System.in);

        System.out.println("What is your name?");
        String username = scanner.nextLine();
        System.out.println("Hello " + username);
    }
}
