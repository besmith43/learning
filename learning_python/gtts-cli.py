#!/usr/bin/env python3

"""
https://pythonprogramminglanguage.com/text-to-speech/
"""

import gtts
import os
tts = gtts.tts.gTTS(text='Blake', lang='en')
tts.save("good.mp3")
os.system("vlc good.mp3")
