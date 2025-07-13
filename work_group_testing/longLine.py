#!/usr/bin/env python


item_price = 1.50
item_quantity = 20
discount_percentage = 0.10
tax_rate = 0.03

# Example with parentheses for a long calculation
total_price = (
    item_price * item_quantity
    - (item_price * item_quantity * (discount_percentage / 100))
    + (item_price * item_quantity * tax_rate)
)

print(f"total price: {total_price}")

# Example with brackets for a list
my_list = [
    "apple",
    "banana",
    "cherry",
    "date",
    "elderberry",
    "fig",
]

for item in my_list:
    print(f"my_list item: {item}")

# Example with braces for a dictionary
my_dict = {
    "key1": "value1",
    "key2": "value2",
    "key3": "value3",
}

for key in my_dict:
    print(f"my_dict key: {key}")
    print(f"my_dict value: {my_dict[key]}")


# Example with backslash for a long string
long_message = "This is a very long string that needs to be broken \
over multiple lines for better readability."

print(f"long message: {long_message}")

# Example with backslash for a long expression
result = 1 + \
         2 + \
         3

print(f"result: {result}")

multi_line_string = (
    "This is the first part of the string. "
    "This is the second part of the string. "
    "And this is the final part."
)


print(f"multi line string: {multi_line_string}")


