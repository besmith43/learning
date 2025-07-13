#!/usr/bin/env python



user = {
    "name": "Bob",
    "username": "bbb",
    "job_title": "builder",
    "city": "Washington DC",
}


try:
    print(f"name: {user['name']}")
    name = user['name']
    print(f"username: {user['username']}")
    print(f"job_title: {user['job_title']}")
    print(f"city: {user['city']}")
    print(f"email: {user['email']}")
except KeyError as e:
    print("key error block")
    print(f"caught exception: {e}")
    print(f"type of  exception: {type(e)}")
    print(f"local name variable: {name}")
except Exception as e:
    print(f"caught exception: {e}")
    print(f"type of  exception: {type(e)}")
    print(f"local name variable: {name}")


