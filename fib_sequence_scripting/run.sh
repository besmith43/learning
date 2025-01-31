#!/usr/bin/env bash


x=10

log="./logs/output.log"

echo -e "" > $log

echo "dotnet" >> $log
# time ./main.csx >> $log 2>&1
# time ./main.csx 2>&1 | tee -a $log
{ time ./main.csx $x; } >> $log 2>> $log

echo "" >> $log
echo "" >> $log

echo "rust" >> $log
# time ./main.crs >> $log 2>&1
{ time ./main.crs $x; } >> $log 2>> $log

echo "" >> $log
echo "" >> $log

echo "python" >> $log
# time ./main.crs >> $log 2>&1
{ time ./main.py $x; } >> $log 2>> $log

echo "" >> $log
echo "" >> $log

echo "golang" >> $log
# time ./main.crs >> $log 2>&1
{ time go run ./main.go $x; } >> $log 2>> $log


cat $log

