import tkinter as tk
from tkinter import ttk
import os

def Map_Now():
    os.system('powershell.exe net use /delete ' + drive_letter.get() + ': >nul')
    os.system('powershell.exe net use ' + drive_letter.get() + ': \\\\10.0.1.2\\' + share.get() + ' /persistent:no /USER:besmith crap4m32')
    os.system('explorer.exe ' + drive_letter.get() + ':\\')

win = tk.Tk()
win.title("Mapping Shares")

drive_letter_label = ttk.Label(win, text="Select Drive Letter", width=20)
drive_letter_label.grid(column=0, row=0)

drive_letter = tk.StringVar()
drive_letter_chosen = ttk.Combobox(win, width=12, textvariable=drive_letter, values=('R','S','T','U','V','W','X','Y','Z'), state='readonly')
drive_letter_chosen.grid(column=0,row=1)
drive_letter_chosen.current(8)

share_label = ttk.Label(win, text="Select Share", width=20)
share_label.grid(column=1,row=0)

share = tk.StringVar()
share_chosen = ttk.Combobox(win, width=12, textvariable=share, values=("Docs", "video", "music", "photo", "Windows_Backup"), state='readonly')
share_chosen.grid(column=1,row=1)
share_chosen.current(0)

action = ttk.Button(win, text="Map Share", command=Map_Now)
action.grid(column=1, row=2)

win.mainloop()
