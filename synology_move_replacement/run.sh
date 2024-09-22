#!/bin/bash

SRC="testfile"
DEST="testdest"


if [ ! -f $SRC ]; then
	echo creating $SRC
	# touch $SRC
	# 1k
	# head -c 1024 /dev/urandom > $SRC
	# 1M
	# head -c 1048576 /dev/urandom > $SRC
	# 1G
	head -c 1073741824 /dev/urandom > $SRC
fi


if [ ! -d $DEST ]; then
	mkdir $DEST
fi


go run main.go $SRC $DEST

echo
echo

ls -alh $DEST

echo
echo

rm -r $DEST

if [ -f $SRC ]; then
	rm $SRC
fi


