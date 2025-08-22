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


// ternary conditional operator

let myAge = 18

var canVote = age >= 18 ? "Yes" : "No"

// basically if/else
canVote = ""

if age >= 18 {
    canVote = "Yes"
} else {
    canVote = "No"
}


// loops

let platforms = ["iOS", "macOS", "tvOS", "watchOS"]

for os in platforms {
    print("Swift works great on \(os).")
}

// range - 1...12 (inclusive)
for i in 1...12 {
    print(i)
}

for i in 1..<5 {
    print("counting from 1 up to 5: \(i)")
}

for _ in 1...5 {
    print("don't care about the loop var")
}

var while_count = 0
while while_count < 10 {
    print("while loop")
    while_count += 1
}

// skipped break and continue... let's face it.. that's easy

// checkpoint 3

// fizzbuzz for 1 through 100

for i in 1...100 {
    if i.isMultiple(of: 3) && i.isMultiple(of: 5) {
        print("fizzbuzz")
    } else if i.isMultiple(of: 3) {
        print("fizz")
    } else if i.isMultiple(of: 5) {
        print("buzz")
    } else {
        print(i)
    }
}

// functions

// overloading is a thing... interesting
func hello() {
    print("Hello world")
}

func hello(name: String) {
    print("Hello \(name)")
}

hello()
hello(name: "Bob") // must have the variable name in the calling of the function... boo...

// returns

func hello2() -> String {
    return "Hello World"
}

print(hello2())

// NOTE: return keyword is optional like in rust
// I don't like it...
func getUser() -> [String] {
    ["Taylor", "Swift"]
}

let user = getUser()
print("Name: \(user[0]) \(user[1])")


func getUser_dict() -> [String: String] {
    [
        "firstName": "Taylor",
        "lastName": "Swift"
    ]
}

let user_dict = getUser_dict()
print("Name: \(user_dict["firstName", default: "Anonymous"]) \(user_dict["lastName", default: "Anonymous"])")


func getUser_tuple() -> (firstName: String, lastName: String) {
    (firstName: "Taylor", lastName: "Swift")
}

let user_tuple = getUser_tuple()
print("Name: \(user_tuple.firstName) \(user_tuple.lastName)")


// customize parameter labels

// use _ to make a parameter label default

func printTimesTable(number: Int) {
    for i in 1...12 {
        print("\(i) x \(number) is \(i * number)")
    }
}

func printTimesTable(_ number: Int) {
    for i in 1...12 {
        print("\(i) x \(number) is \(i * number)")
    }
}

func printTimesTable(for number: Int) {
    for i in 1...12 {
        print("\(i) x \(number) is \(i * number)")
    }
}

printTimesTable(5)
printTimesTable(for: 5)
// printTimesTable(number: 5) // only good as an internal name unless I make a 3rd overload
printTimesTable(number: 5)


enum PasswordError: Error {
    case short, obvious
}

func checkPassword(_ password: String) throws -> String {
    if password.count < 5 {
        throw PasswordError.short
    }
    
    if password == "12345" {
        throw PasswordError.obvious
    }
    
    if password.count < 8 {
        return "OK"
    } else if password.count < 10 {
        return "Good"
    } else {
        return "Excellent"
    }
}

let password = "12345"

do {
    let result = try checkPassword(password)
    print("Password rating: \(result)")
} catch PasswordError.short {
    print("please use a longer password")
} catch PasswordError.obvious {
    print("I have the same combination on my luggage")
} catch {
    print("there was an error: \(error.localizedDescription)")
}



// checkpoint 4

enum MyError: Error {
    case outofbounds, notfound
}

func square_root(_ num: Int) throws -> Int {
    if num < 1 || num > 10_000 {
        throw MyError.outofbounds
    }
    
    
    for i in 1...100 {
        if i*i == num {
            return i
        }
    }
    
    throw MyError.notfound
}

do {
    let ans = try square_root(25)
    print("square root is \(ans)")
} catch MyError.outofbounds {
    print("given number was out of bounds")
} catch MyError.notfound {
    print("square root not found")
} catch {
    print("there was an error: \(error.localizedDescription)")
}


// closures
// aka function variables

func greetUser() {
    print("Hi there!")
}

greetUser()

var greetCopy = greetUser
greetCopy()

let sayHello = {
    print("Hi there!")
}

sayHello()



let sayHello2 = { (name: String) -> String in
    "Hi \(name)!"
}

print(sayHello2("Bob"))



struct Album {
    let title: String
    let artist: String
    let year: Int
    
    func printSummary() {
        print("\(title) (\(year)) by \(artist)")
    }
}

let red = Album(title: "Red", artist: "Taylor Swift", year: 2012)
let wings = Album(title: "Wings", artist: "BTS", year: 2016)

print(red.title)
print(wings.artist)

red.printSummary()
wings.printSummary()


struct Employee {
    let name: String
    var vacationRemaining: Int
   
    /*
        reading from self is fine
        but to mutate self, you need the mutating keyword
     */
    mutating func takeVacation(days: Int) {
        if vacationRemaining > days {
            vacationRemaining -= days
            print("I'm going on vaction!")
            print("Days remaining: \(vacationRemaining)")
        } else {
            print("Oops! there aren't enough days remaining")
        }
    }
}

var archer = Employee(name: "Sterling Archer", vacationRemaining: 14)
// let archer = Employee(name: "Sterling Archer", vacationRemaining: 14)
archer.takeVacation(days: 5)
print(archer.vacationRemaining)


// computed property

struct cmp_Employee {
    let name: String
    var vacationAllocated = 14
    var vacationTaken = 0
    
    var vacationRemaining: Int {
        get {
            vacationAllocated - vacationTaken
        }
        
        set {
            vacationAllocated = vacationTaken + newValue
        }
    }
}

var archer2 = cmp_Employee(name: "Sterling Archer", vacationAllocated: 14)
archer2.vacationTaken += 4
archer2.vacationRemaining = 5
print(archer2.vacationAllocated)


// property observers

struct Game {
    var score = 0 {
        didSet {
            print("Score is now \(score)")
        }
    }
}

var game = Game()
game.score += 10
game.score -= 3
game.score += 1


struct App {
    var contacts = [String]() {
        willSet {
            print("Current Value is: \(contacts)")
            print("New Value will be: \(newValue)")
        }
        
        didSet {
            print("There are now \(contacts.count) contacts")
            print("Old Value was: \(oldValue)")
        }
    }
}


var app = App()
app.contacts.append("Adrian E")
app.contacts.append("Allen W")
app.contacts.append("Ish S")


// custom initializers


struct Player {
    let name: String
    let number: Int
   
    // note that it doesn't have a func keyword
    // also there's no return value
    init(name: String, number: Int) {
        self.name = name
        self.number = number
    }
    
    init(name: String) {
        self.name = name
        number = Int.random(in: 1...99)
    }
}

// let player = Player(name: "Megan R", number: 15)
let player = Player(name: "Megan R")
print(player.number)



