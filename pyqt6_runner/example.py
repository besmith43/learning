#!/usr/bin/env -S uv run --script


# /// script
# requires-python = ">=3.14"
# dependencies = [
#     "pyqt6>=6.11.0",
# ]
# ///


import sys
from PyQt6.QtWidgets import QApplication, QWidget, QLabel, QVBoxLayout

def main():
    # 1. Initialize the application object
    app = QApplication(sys.argv)
    
    # 2. Create the main application window (Widget)
    window = QWidget()
    window.setWindowTitle('PyQt6 Setup Test')
    window.resize(300, 200)
    
    # 3. Create a layout and add a text label
    layout = QVBoxLayout()
    label = QLabel('PyQt6 is successfully configured!')
    layout.addWidget(label)
    window.setLayout(layout)
    
    # 4. Windows are hidden by default, so explicitly display it
    window.show()
    
    # 5. Start the Qt application event loop
    sys.exit(app.exec())

if __name__ == '__main__':
    main()
