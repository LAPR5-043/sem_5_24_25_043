#!/bin/bash

# Limpar regras anteriores
ip6tables -F

# Permitir localhost
ip6tables -A INPUT -i lo -j ACCEPT

# Permitir acesso da rede interna do DEI para todos os protocolos
# Rede para Estudantes
ip6tables -A INPUT -s fd1e:2bae:c6fd:1008::/64 -j ACCEPT
# Rede para Professores
ip6tables -A INPUT -s fd1e:2bae:c6fd:1004::/64 -j ACCEPT

# Definir default policy para DROP
ip6tables -P INPUT DROP