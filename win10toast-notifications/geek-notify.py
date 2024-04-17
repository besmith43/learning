#!/usr/bin/env python3

# import win10toast 
from win10toast import ToastNotifier
  
# create an object to ToastNotifier class
n = ToastNotifier()
  
n.show_toast("GEEKSFORGEEKS", "You got notification", duration = 10,
 icon_path ="https://media.geeksforgeeks.org/wp-content/uploads/geeks.ico")
