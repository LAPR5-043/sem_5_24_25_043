# Limpar regras anteriores
sudo iptables -F

# Permitir localhost
sudo iptables -A INPUT -i lo -j ACCEPT

# Permitir acesso da rede cablada do DEI
#sudo iptables -A INPUT -p tcp -s (ver ip do dei) -j ACCEPT

# Permitir acesso da rede VPN do DEI
sudo iptables -A INPUT -p tcp -s 10.8.0.0/16 -j ACCEPT

# Bloquear o resto
sudo iptables -A INPUT -p tcp -j DROP
