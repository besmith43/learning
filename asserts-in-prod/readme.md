# Asserts in Prod

The idea here is to have asserts in your code that run all the time so that you can assert that things are the way that they're supposed to be.
For example, if you have a network connection that's already closed, why would you call the close method?
In this situation, it would be ideal, in a sense, to crash and make it known that something that should have never happened, just happened.
Now obviously this can be taken to an extreme, but the basic idea comes from the [tiger beetle team](https://github.com/tigerbeetle/tigerbeetle/blob/main/docs/TIGER_STYLE.md)


### Making your own asserts

So.... you can make your own assert functions.
However the annoyance involved is real, but it'll give you the ability to be graceful about the failure.
I don't know which is more useful:

1. gracefully exiting early
2. immediate crashing due to the assert! macro

Either way, it ain't all rainbows and roses.



