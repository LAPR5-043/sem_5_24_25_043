#!/bin/bash

# Certificar que limpamos o ficheiro
> /etc/ssh/banner.txt

# Obter a data atual
DATE=$(date "+%Y-%m-%d %H:%M:%S")

# Obter o número de users ativos (example: users with /bin/bash shell)
ACTIVATED_USERS=$(who | wc -l)


# Criar a mensagem do banner
MESSAGE=$(cat <<EOF
==============================================
  Hello mate, It seems that you are new here
  If you want to explore the system, you will
    need to login first. Enjoy your stay!

    Remember, you are not alone here!
    Respect the others and have fun!
==============================================

  📅 Date and Time:     $CURRENT_DATE


  How many nerds you can meet right now:

  👥 Logged In Users:   $ACTIVATED_USERS

==============================================
EOF
)

# Certificar que temos permisssões para escrever 
chmod 644 /etc/ssh/banner.txt
chmod 644 /etc/issue

{
  echo -e "$MESSAGE" | /usr/games/cowsay -W 46 -n -f tux
} > /etc/ssh/banner.txt 


