#!/usr/bin/env xonsh

1 + 1

import requests
  requests.get("https://xon.sh").status_code

print(1 if True else 2)

for i, x in enumerate('xonsh'):
    # For easier indentation, Shift+Tab will enter 4 spaces.
    print(i, x)

def f():
    return "xonsh"
f()

# On the other hand, you can execute commands:

echo hello
# cd $HOME
id $(whoami) > id.txt
cat id.txt | grep "besmith"

# Finally, you can use everything together:

name = 'snail'
echo @(name) > /tmp/@(name)

$PATH.append('/tmp')

@.imp.json.loads($(echo '{"a":1}'))
