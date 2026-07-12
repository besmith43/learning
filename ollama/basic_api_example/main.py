import os
import sys
from ollama import Client

if 'OLLAMA_API_KEY' not in os.environ:
    print('you are missing the OLLAMA_API_KEY as an env variable')
    sys.exit(1)



client = Client(
    host='https://ollama.com',
    headers={'Authorization': 'Bearer ' + os.environ.get('OLLAMA_API_KEY')}
)

messages = [
  {
    'role': 'user',
    'content': 'what is the series title, season number, and episode number in this directory name: 12.Monkeys.S04E01.The.End.1080p.BluRay.Dts-HDMa5.1.AVC-PiR8\noutput in the following format: SERIES TITLE - s00e00',
  },
]

for part in client.chat('nemotron-3-nano:30b-cloud', messages=messages, stream=True):
  print(part.message.content, end='', flush=True)
