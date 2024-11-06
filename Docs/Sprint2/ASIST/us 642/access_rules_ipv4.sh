#!/bin/bash

# Limpar regras anteriores
iptables -F

# Permitir localhost
iptables -A INPUT -i lo -j ACCEPT

# Permitir acesso da rede interna do DEI para todos os protocolos
# Rede para Estudantes
iptables -A INPUT -s 10.8.0.0/16 -j ACCEPT
# Rede para Professores
iptables -A INPUT -s 10.4.0.0/16 -j ACCEPT

# Definir default policy para DROP
iptables -P INPUT DROP