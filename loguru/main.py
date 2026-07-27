#!/usr/bin/env python

from loguru import logger
import sys

def main():

    logger.remove(0)
    logger.add(sys.stderr, level="TRACE", format="{time} | {level} | {name}.{function}:{line} | {message} | {extra}")

    logger.info("Hello World!")
    logger.trace("a trace message")
    logger.debug("a debug message")
    logger.success("a success message")
    logger.warning("a warning message")
    logger.error("an error message")
    logger.critical("a critical message")

    childLogger = logger.bind(seller_id="001", product_id="123")
    childLogger.info("product page opened")


if __name__ == "__main__":
    main()

