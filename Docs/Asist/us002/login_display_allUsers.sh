#!/bin/bash

# Show the locomotive
sl -e -w

# Get the username
USERNAME=$(whoami)

# Get Last Login from the User
LAST_LOGIN=$(last -1 $(whoami) | awk '{print $4, $5, $6, $7, $8}' | tail -n 1)

# Get the machine uptime
UPTIME=$(uptime -p)

# Concatenate the message
MESSAGE="Welcome, $USERNAME! \n\nThe current date and time is $(date '+%d/%m/%Y %H:%M:%S')\nYour last login was on: $LAST_LOGIN\nCurrent Machine uptime: $UPTIME"

# Display the message
echo -e "$MESSAGE" | cowsay -W 46 -n -f tux