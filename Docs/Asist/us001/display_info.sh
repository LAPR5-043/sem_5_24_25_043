#!/bin/bash

# Certificar que limpamos o ficheiro
> /etc/ssh/banner.txt
> /etc/issue

# Obter a data atual
DATE=$(date "+%Y-%m-%d %H:%M:%S")

# Obter o número de users ativos (example: users with /bin/bash shell)
ACTIVATED_USERS=$(grep -c '/bin/bash' /etc/passwd)

# Criar a mensagem do banner
BANNER_MESSAGE=$(cat <<EOF
========================================
Welcome to the server!
Please respect the rules!
Never share your password with anyone!
Current Date: $DATE
Number of Activated Users: $ACTIVATED_USERS
========================================
EOF
) 

{
  echo "$BANNER_MESSAGE"
} > /etc/ssh/banner.txt 


{
  echo "$BANNER_MESSAGE"
} > /etc/issue


# Certificar que temos permisssões para escrever 
chmod 644 /etc/ssh/banner.txt
chmod 644 /etc/issue