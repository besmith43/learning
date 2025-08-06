import Cocoa


// let vs var

/*
 
    let creates a constaint (immutable data)
 
    var creates a variable (mutable data)
 
 */


// strings

var greeting = "Hello, playground"

var multiline_string = """
    this is a test 
    of the emergency broadcast system
    """


// numbers

let score = 10

let reallyBig = 10_000_000

let lowerScore = score - 2
let higherScore = score + 10
let doubledScore = score * 2
let squaredScore = score * score
let halvedScore = score / 2
print(score)

var counter = 10

counter += 5
print(counter)

let number = 120
print(number.isMultiple(of: 3))

print(120.isMultiple(of: 3))


// decimals / floats

let float_number = 0.1 + 0.2

print(float_number)

let a = 1
let b = 2.0
let c = Double(a) + b

print(c)

/* cgfloat stands for core graphics float and swift handles that and doubles interchangably */


// booleans

let filename = "paris.jpg"
print(filename.hasSuffix(".jpg"))

let goodDogs = true
let gameOver = false

let isMultiple = 120.isMultiple(of: 3)

var isAuthenticated = false
isAuthenticated = !isAuthenticated
print(isAuthenticated)
isAuthenticated.toggle()
print(isAuthenticated)


// string interplation

var string1 = "hello, "
var string2 = "world"

var greeting2 = string1 + string2
print(greeting2)


let quote = "the he tapped a sign saying \"believe\" and walked away"
print(quote)

let name = "taylor"
let age = 26
let message = "hello, my name is \(name) and I'm \(age) years old."

print(message)


// checkpoint 1

let cDegrees = 30.0

let fDegrees = cDegrees*9/5+32

print("Celsius: \(cDegrees)\nFahrenheit: \(fDegrees)")


// arrays

var beatles = ["John", "Paul", "George", "Ringo"]
let arr_numbers = [4, 8, 16, 32, 42]
let temps = [25.3, 28.2, 26.4]


print(beatles[0])

beatles.append("Adrian")


var scores = Array<Int>()
scores.append(100)
scores.append(80)
scores.append(60)
scores.append(40)
scores.append(20)

print(scores)

// shorthand

var albums = [String]()
albums.append("Folklore")
albums.append("Fearless")
albums.append("Red")

print(albums)

// https://developer.apple.com/documentation/swift/array

let cities = ["London", "Toyko", "Rome", "Budapest"]
print(cities.sorted())

print(cities.reversed())
// print(cities.reverse())
// can't use reverse as it mutates the array and cities is immutable


// dictionaries

var employee = ["name": "Taylor Swift", "job": "Singer", "location": "Nashville"]
print(employee["password"])
print(employee["status"])
print(employee["manager"])// getting an optional



print(employee["name", default: "Unknown"])
print(employee["job", default: "Unknown"])
print(employee["location", default: "Unknown"])


var heights = [String:Int]()
heights["Yao Ming"] = 229
heights["Shaquille O'Neal"] = 216
heights["LeBron James"] = 206







