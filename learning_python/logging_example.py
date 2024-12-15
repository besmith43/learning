#!/usr/bin/env python

"""
see the official python docs for more info
http://docs.python.org/2/howto/logging.html#logging-basic-tutorial
http://docs.python.org/2/howto/logging.html#logging-advanced-tutorial
http://docs.python.org/2/howto/logging-cookbook.html#logging-cookbook

this example is from
https://fangpenlin.com/posts/2012/08/26/good-logging-practice-in-python/
"""

import logging

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

handler = logging.FileHandler('logging_example.log')
handler.setLevel(logging.INFO)

formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
handler.setFormatter(formatter)

logger.addHandler(handler)

logger.info("starting program")
logger.debug("here's an error message")
# etc