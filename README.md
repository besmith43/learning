# Learning

### Summary

To setup a custom manpath on a per user basis, create a ```~/.manpath``` file and place the desired path preceeded by MANDATORY_PATH.
A little something like this:

```
MANDATORY_PATH /Users/besmith/.local/share/man
```

Now at the end of this path, you'll need a man1 directory and every man page within that directory needs to have a .1 extension.
Then for every additional man page type (1-8), you'll need to follow the same pattern.

[creating a manpage](https://www.cyberciti.biz/faq/linux-unix-creating-a-manpage/)
