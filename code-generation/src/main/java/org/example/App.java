package org.example;

/**
 * Hello world!
 *
 */
public class App 
{
    public static void main( String[] args )  {
        System.out.println( "Hello World!" );

        Person person = new Person();
        person.FirstName = "John";
        person.LastName = "Doe";

        System.out.println(person.toString());
    }
}

