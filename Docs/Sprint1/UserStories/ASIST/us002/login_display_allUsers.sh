#!/bin/bash

# Show the locomotive
sl -e -w

# Get the username
USERNAME=$(whoami)

# Get the current date and time
CURRENT_DATE=$(date '+%d/%m/%Y %H:%M:%S')

# Get Last Login from the User
LAST_LOGIN=$(last -F -n 2 "$USERNAME" | awk 'NR==2 {print $4, $5, $6, $7, $8, $9}')

# Format Last Login
LAST_LOGIN_FORMATTED=$(date -d "$LAST_LOGIN" '+%a %b %d %H:%M:%S %z %Y')

# Get the machine uptime
UPTIME=$(uptime -p)

# Get system load
LOAD=$(uptime | awk -F'load average:' '{ print $2 }' | sed 's/^ //')

# Get disk usage
DISK_USAGE=$(df -h / | awk 'NR==2 {print $5 " used, " $4 " available"}')

# Get memory usage
MEMORY=$(free -h | awk '/Mem:/ {print $3 " used, " $4 " available"}')

# Get number of logged-in users
LOGGED_USERS=$(who | wc -l)

# Get IP address
IP_ADDRESS=$(hostname -I | awk '{print $1}')

# Concatenate the message with formatting
MESSAGE=$(cat <<EOF
==============================================
           Welcome, $USERNAME!
==============================================

  📅 Date and Time:     $CURRENT_DATE
  🔑 Last Login:        $LAST_LOGIN_FORMATTED
  ⏳ Uptime:            $UPTIME

  📊 System Load:       $LOAD
  💾 Disk Usage:        $DISK_USAGE
  🧠 Memory Usage:      $MEMORY

  👥 Logged In Users:   $LOGGED_USERS
  🌐 IP Address:        $IP_ADDRESS

==============================================
EOF
)

# Display the message with cowsay
echo -e "$MESSAGE" | cowsay -W 46 -n -f tux
