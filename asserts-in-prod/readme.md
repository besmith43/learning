# Asserts in Prod

The idea here is to have asserts in your code that run all the time so that you can assert that things are the way that they're supposed to be.
For example, if you have a network connection that's already closed, why would you call the close method?
In this situation, it would be ideal, in a sense, to crash and make it known that something that should have never happened, just happened.
Now obviously this can be taken to an extreme, but the basic idea comes from the [tiger beetle team](https://github.com/tigerbeetle/tigerbeetle/blob/main/docs/TIGER_STYLE.md)




