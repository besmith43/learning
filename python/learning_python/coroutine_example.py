#!/usr/bin/env python3

import asyncio
import datetime
import concurrent.futures

"""
see examples at the following links:
http://blog.mathieu-leplatre.info/some-python-3-asyncio-snippets.html
https://www.pythonsheets.com/notes/python-asyncio.html
https://stackoverflow.com/questions/46074841/why-coroutines-cannot-be-used-with-run-in-executor
"""

async def anything(i):
    print(i, datetime.datetime.now())
    await asyncio.sleep(i)

def anything2(i):
    print(i, datetime.datetime.now())


"""
if __name__ == '__main__':
    loop = asyncio.get_event_loop()
    tasks = [loop.create_task(anything(i)) for i in range(1, 4)]

    try:
        loop.run_until_complete(asyncio.wait(tasks))
    finally:
        loop.close()

"""
# this is supposed to work but fails with an error of ThreadPoolExecutor is not defined?
# however see the link for a potentially working example
# https://pymotw.com/3/asyncio/executors.html

if __name__ == '__main__':
    loop = asyncio.get_event_loop()
    loop.call_later(2, loop.stop)
    executor = concurrent.futures.ThreadPoolExecutor(max_workers=8)
    futures = [loop.run_in_executor(executor, anything2, i) for i in range(1, 4)]
    
    try:
        loop.run_until_complete(asyncio.wait(futures))
    finally:
        loop.close()

