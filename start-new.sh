#!/usr/bin/env bash


if [ -z "$1" ]; then
    echo "you didn't pass in a name for the new branch"
    exit 1
fi


BRANCH="$1"


echo "New Branch Name: $BRANCH"
gum confirm || exit 0


git checkout -b $BRANCH

git checkout master

git worktree add ../learning-$BRANCH $BRANCH


