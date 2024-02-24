#!/bin/bash

go build cmdline_flags.go

./cmdline_flags -word=opt -numb=7 -fork -svar=flag

./cmdline_flags -word=opt

./cmdline_flags -word=opt a1 a2 a3

./cmdline_flags -word=opt a1 a2 a3 -numb=7

./cmdline_flags -h

./cmdline_flags -wat


rm cmdline_flags
