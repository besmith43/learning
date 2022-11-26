/**  this is taken from the youtube video at the link below
*	 https://www.youtube.com/watch?v=WBPrJSw7yQA
*   */

// adding this so that it gets treated as a module instead of a script
export{}

let message = 'Hello World';

console.log(message);

let x = 10;
const y = 20;  // can't be changed and must be given a value when declared

//let x = 30;  // redeclaring a variable in typescipt isn't allowed

let sum = x + y;

console.log(`the sum is ${sum}`);


// Types

let isBeginner: boolean = true;

let total: number = 0;

let name: string = 'Blake';

let sentence: string = `My name is ${name}
I am a beginner in Typescript`;

console.log(sentence);


let n: null = null;

let u: undefined = undefined;

let isNew: boolean = null;

let myName: string = undefined;


// different ways of declaring an array
// there isn't a benefit to one over the other

let list1: number[] = [1,2,3];

let list2: Array<number> = [1,2,3];

// tuple
let person1: [string, number] = ['Chris', 22];


// enum
// this can be done all on one line or separated like I have
enum Color {
	red = 5,
	green,
	blue
}

let c: Color = Color.green;

console.log(c);


let randomValue: any = 10;

randomValue = true;

randomValue = 'Blake';

let myVariable: any = 10;

console.log(myVariable.name);

//myVariable();
//myVariable.toUpperCase();


let myVariable2: unknown = 10;

function hasName(obj: any): obj is { name: string } {
	return !!obj &&
		typeof obj === "object" &&
		"name" in obj
}

if (hasName(myVariable2)){
	console.log(myVariable2.name);
}
// myVariable2();  //with unknown variables you can't call them as functions
// (myVariable2 as string).toUpperCase();  // basically type casting



let a;
a = 10;

a = true;

let b = 20;


// union of types

let multiType: number | boolean;

multiType = 20;
multiType = true;

/*  JavaScript Version
function add(num1, num2) {
	return num1 + num2;
}
*/

function add(num1: number, num2: number): number {
	return num1 + num2;
}

add(5, 10);
// add(5, '10');  using a string will make the function fail

// optional parameters
// add a ? to the end of optional parameters
// must be the last parameters

function add2(num1: number, num2?: number): number {
	if (num2)
		return num1 + num2;
	else
		return num1;
}

console.log(add2(x));


// default parameter values
function add3(num1: number, num2: number = 10): number {
	return num1 + num2;
}

console.log(add3(x));


// Interfaces

interface Person {
	firstName: string;
	lastName?: string;
}

function fullName(person: Person) {
	console.log(`${person.firstName} ${person.lastName}`);
}

let p = {
	firstName: 'Bruce',
	lastName: 'Wayne'
};

fullName(p);

let p2 = {
	firstName: 'Bruce'
};

fullName(p2); // will print "undefined" for the last name


// Classes
// typescript has them but javascript doesn't

class Employee {
	public employeeName: string;

	constructor(name: string) {
		this.employeeName = name;
	}

	greet() {
		console.log(`Good Morning ${this.employeeName}`);
	}
}


let employee1 = new Employee('Bruce');


console.log(employee1.employeeName);
employee1.greet();


// class inheritance


class Manager extends Employee {
	constructor(managerName: string) {
		super(managerName);
	}

	delegateWork() {
		console.log(`Manager delegating tasks`);
	}
}

let m1 = new Manager('Bruce');

m1.delegateWork();
m1.greet();
console.log(m1.employeeName);


// access modifiers
// public, private, and protected keywords




