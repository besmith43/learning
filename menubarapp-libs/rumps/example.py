#!/usr/bin/env -S uv run --script


# /// script
# requires-python = ">=3.14"
# dependencies = [
#     "rumps>=0.4.0",
# ]
# ///


import rumps

class MenuBarApp(rumps.App):
    def __init__(self):
        # Set the name/text displayed in the menu bar
        super(MenuBarApp, self).__init__("🚀 MyMenu")
        
        # Define the items inside the dropdown menu
        self.menu = ["Trigger Action", "Say Hello"]

    @rumps.clicked("Trigger Action")
    def run_action(self, _):
        rumps.alert("Action triggered successfully!")

    @rumps.clicked("Say Hello")
    def say_hello(self, _):
        rumps.notification("Greeting", "Hello World!", "This is a native macOS notification.")

if __name__ == "__main__":
    MenuBarApp().run()

