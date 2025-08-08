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


// set

let actors = Set([
    "Denzel Washington",
    "Tom Cruise",
    "Nicolas Cage",
    "Samuel L Jackson"
])

print(actors)

var actors2 = Set<String>()
actors2.insert("Denzel Washington")
actors2.insert("Tom Cruise")
actors2.insert("Nicolas Cage")
actors2.insert("Samuel L Jackson")
print(actors2)


// enums

enum WeekDay {
    case monday
    case tuesday
    case wednesday
    case thursday
    case friday
}

var day = WeekDay.monday
day = WeekDay.tuesday
day = WeekDay.friday

print(day)


enum WeekDay2 {
    case monday, tuesday, wednesday, thursday, friday
}


// type annotations

let surname: String = "Lasso"
let score3: Int = 0
let score4: Double = 0


// empty array

var teams = [String]()
// or
var teams2: [String] = [String]()


enum UIStyle {
    case light, dark, system
}

var style = UIStyle.light
style = .dark  // shorthand


let username: String
// do something
username = "@twostraws"
// swift lets you declare a constaint before you assign to it
//       only allowed to assign to a constaint once
print(username)


// checkpoint 2

var check2_arr = [String]()
check2_arr.append("Bob")
check2_arr.append("Bob")
check2_arr.append("Charlie")
check2_arr.append("Mark")
check2_arr.append("Mike")
check2_arr.append("Ryan")

print(check2_arr.count)
print(Set(check2_arr).count)


// if else

if true {
    print("whatever is true")
}


let if_score = 85

if if_score > 80 {
    print("great job")
}


// multiple conditions

let mc_age = 16

if mc_age >= 18 {
    print("you can vote in the next election.")
} else {
    print("Sorry, you're too young to vote")
}

let temp = 25

if temp > 20 && temp < 30 {
    print("it's a nice day")
}


let userAge = 14
let hasParentalConsent = true

if age >= 18 || hasParentalConsent {
    print("you can buy the game!")
}


enum TransportOption {
    case airplane, helicopter, bicycle, car ,escooter
}

let transport = TransportOption.airplane

if transport == .airplane || transport == .helicopter {
    print("let's fly")
} else if transport == .bicycle {
    print("I hope there's a bike path...")
} else if transport == .car {
    print("Time to get stuck in traffic")
} else {
    print("I'm going to hire a scooter now!")
}


// switch statements


enum Weather {
    case sun, rain, wind, snow, unknown
}

let weather = Weather.sun

switch(weather) {
case .sun:
    print("it should be a nice day")
    break
case .rain:
    print("pack an umbrella")
    break
case .wind:
    print("wear something warm")
    break
case .snow:
    print("school is cancelled")
    break
case .unknown:
    print("our forecast generator is broken")
    break
}


let place = "Metropolis"

switch place {
case "Gotham":
    print("you're batman!")
case "Mega-City One":
    print("you're judge dredd!")
case "Wakanda":
    print("you're black panther!")
default:
    print("who are you?")
}

// 12 days of christmas

let days_of_christmas = 5

print("my true love gave to me...")

switch days_of_christmas {
case 5:
    print("5 golden rings")
    fallthrough
case 4:
    print("4 calling birds")
    fallthrough
case 3:
    print("3 French hens")
    fallthrough
case 2:
    print("2 turtle doves")
    fallthrough
default:
    print("A partridge in a pear tree")
}






