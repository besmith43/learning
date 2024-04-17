# Learning

### Summary

This repo is a test bed for various things that I'm playing with in my freetime.

### How I'm using it

I've broken these from being a mono repo into a branch dedicated to each language/runtime/tool and using git worktree's I've got them as separate folders on my machine.

```bash
    git checkout -b new-lang
    git checkout master
    git worktree add ../learning-new-lang new-lang
```
