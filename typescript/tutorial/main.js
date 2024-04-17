"use strict";
/**  this is taken from the youtube video at the link below
*	 https://www.youtube.com/watch?v=WBPrJSw7yQA
*   */
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
exports.__esModule = true;
var message = 'Hello World';
console.log(message);
var x = 10;
var y = 20; // can't be changed and must be given a value when declared
//let x = 30;  // redeclaring a variable in typescipt isn't allowed
var sum = x + y;
console.log("the sum is ".concat(sum));
// Types
var isBeginner = true;
var total = 0;
var name = 'Blake';
var sentence = "My name is ".concat(name, "\nI am a beginner in Typescript");
console.log(sentence);
var n = null;
var u = undefined;
var isNew = null;
var myName = undefined;
// different ways of declaring an array
// there isn't a benefit to one over the other
var list1 = [1, 2, 3];
var list2 = [1, 2, 3];
// tuple
var person1 = ['Chris', 22];
// enum
// this can be done all on one line or separated like I have
var Color;
(function (Color) {
    Color[Color["red"] = 5] = "red";
    Color[Color["green"] = 6] = "green";
    Color[Color["blue"] = 7] = "blue";
})(Color || (Color = {}));
var c = Color.green;
console.log(c);
var randomValue = 10;
randomValue = true;
randomValue = 'Blake';
var myVariable = 10;
console.log(myVariable.name);
//myVariable();
//myVariable.toUpperCase();
var myVariable2 = 10;
function hasName(obj) {
    return !!obj &&
        typeof obj === "object" &&
        "name" in obj;
}
if (hasName(myVariable2)) {
    console.log(myVariable2.name);
}
// myVariable2();  //with unknown variables you can't call them as functions
// (myVariable2 as string).toUpperCase();  // basically type casting
var a;
a = 10;
a = true;
var b = 20;
// union of types
var multiType;
multiType = 20;
multiType = true;
/*  JavaScript Version
function add(num1, num2) {
    return num1 + num2;
}
*/
function add(num1, num2) {
    return num1 + num2;
}
add(5, 10);
// add(5, '10');  using a string will make the function fail
// optional parameters
// add a ? to the end of optional parameters
// must be the last parameters
function add2(num1, num2) {
    if (num2)
        return num1 + num2;
    else
        return num1;
}
console.log(add2(x));
// default parameter values
function add3(num1, num2) {
    if (num2 === void 0) { num2 = 10; }
    return num1 + num2;
}
console.log(add3(x));
function fullName(person) {
    console.log("".concat(person.firstName, " ").concat(person.lastName));
}
var p = {
    firstName: 'Bruce',
    lastName: 'Wayne'
};
fullName(p);
var p2 = {
    firstName: 'Bruce'
};
fullName(p2); // will print "undefined" for the last name
// Classes
// typescript has them but javascript doesn't
var Employee = /** @class */ (function () {
    function Employee(name) {
        this.employeeName = name;
    }
    Employee.prototype.greet = function () {
        console.log("Good Morning ".concat(this.employeeName));
    };
    return Employee;
}());
var employee1 = new Employee('Bruce');
console.log(employee1.employeeName);
employee1.greet();
// class inheritance
var Manager = /** @class */ (function (_super) {
    __extends(Manager, _super);
    function Manager(managerName) {
        return _super.call(this, managerName) || this;
    }
    Manager.prototype.delegateWork = function () {
        console.log("Manager delegating tasks");
    };
    return Manager;
}(Employee));
var m1 = new Manager('Bruce');
m1.delegateWork();
m1.greet();
console.log(m1.employeeName);
// access modifiers
// public, private, and protected keywords
