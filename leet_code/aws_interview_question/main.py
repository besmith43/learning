#!/usr/bin/env python3

# GOAL
# 
# read in the start and end logs
# get the average duration
# 
# 
# output should look like this
# 10.0.0.1,18
# 10.0.0.2,9
# 
# 
# 

from datetime import datetime


def run(start_log: str, end_log: str):
    filename = start_log

    with open(filename) as f:
        start_log_content = f.readlines()

    # print(content)


    filename = end_log

    with open(filename) as f:
        end_log_content = f.readlines()

    # print(content)

    result = format(start_log_content, end_log_content)

    average_collection = get_average(result)

    for item in list(average_collection.values()):
        print(f"{item[2]},{item[1].seconds}")
    # end for loop
# end run


def format(start_log: list[str], end_log: list[str]):
    # print("formatting")
    # print(start_log)
    # print(end_log)

    # collector = [] # making a list
    collector = {} # making a list
    final_collector = {} # making a hashmap/dictionary

    # print("Start Log")
    for entry in start_log:
        # print(entry.split(','))
        split_entry = entry.split(',')
        # add to dictionary
        # collector[key] = value
        # using ip as a key
        collector[split_entry[0]] = split_entry
    # end for loop

    # for item in list(collector.values()):
        # print(item)
    # end for loop

    # print("End Log")
    for entry in end_log:
        # print(entry.split(','))
        split_entry = entry.split(',')

        val = collector.get(split_entry[0])
        # print("val: ")
        # print(val)

        if val != None:
            # array 0=> transaction id, 1=> datetime, 2=> ip
            if val[0] == split_entry[0]:
                # print("found a match")
                # print("end log entry")
                # print(split_entry)
                # print("start log entry")
                # print(val)

                val_datetime = datetime.strptime(val[1], '%m/%d/%y %H:%M:%S')
                split_entry_datetime = datetime.strptime(split_entry[1], '%m/%d/%y %H:%M:%S')

                duration = split_entry_datetime - val_datetime
                # print(duration)

                val[1] = duration
                val[2] = val[2].replace('\n', '')
                final_collector[val[0]] = val
            # end if
        # end if
    # end for loop
    
    return final_collector
# end format


def get_average(collection):
    final = {}
    count = {}

    for item in list(collection.values()):
        if final.get(item[2]) == None:
            final[item[2]] = item
            count[item[2]] = 1
        else:
            tmp = final[item[2]]
            
            if tmp[0] != item[0]:
                count[item[2]] = count.get(item[2]) + 1 # ignore the error
                tmp[1] = tmp[1] + item[1]
                final[tmp[2]] = tmp
            # end if
        # end if/else
    # end for loop

    for item in list(final.values()):
        item[1] = item[1] / count[item[2]]
    # end for loop

    return final
# end get_average

if __name__ == "__main__":
    run("start.log", "end.log")




