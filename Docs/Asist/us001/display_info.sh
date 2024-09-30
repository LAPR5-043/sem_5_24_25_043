#!/bin/bash

# Get the current date
DATE=$(date "+%Y-%m-%d %H:%M:%S")

# Get the number of activated users (example: users with /bin/bash shell)
ACTIVATED_USERS=$(grep -c '/bin/bash' /etc/passwd)

# Message
echo "========================================"
echo "Welcome to the server!"
echo "Please respect the rules!"
echo "Never share your password with anyone!"
echo "Current Date: $DATE"
echo "Number of Activated Users: $ACTIVATED_USERS"
echo "========================================"

exec /bin/bash