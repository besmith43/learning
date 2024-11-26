#!/usr/bin/env kotlin


println("Hello ${args[0]}")

var x = 5

// can't do "is null"
// have to do "is type"
if (x is Int) {
	println("x is not null")
}


// enums

enum class State {
    IDLE, RUNNING, FINISHED
}

val state = State.RUNNING
val message = when (state) {
    State.IDLE -> "It's idle"
    State.RUNNING -> "It's running"
    State.FINISHED -> "It's finished"
}
println(message)
