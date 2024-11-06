# US 6.4.2

## 1. Requirements

**US 6.4.2** Como administrador do sistema quero que apenas os clientes da rede interna do DEI (cablada ou via VPN) possam aceder à solução

**Acceptance Criteria:**

- 642.1 - Os utilizadores que se encontrem na rede interna do DEI (cablada ou via VPN) podem aceder à solução

- 642.2 - Os utilizadores que não se encontrem na rede interna do DEI (cablada ou via VPN) não podem aceder à solução


## 2. Analysis

### 2.1. Requirement

The system shall only allow users from the DEI internal network (wired or via VPN) to access the solution. Users who are not on the DEI internal network (wired or via VPN) should not be able to access the solution.

### 2.2 DEI's Internal Network

The DEI's internal network is a private network that is only accessible to users who are physically connected to the DEI network or who are connected via VPN. 

#### 2.2.1. Network Ranges

The DEI's internal network, both wired and via VPN, uses the following IP ranges:

- Wired/VPN Network for Students: 10.8.0.0/16,  fd1e:2bae:c6fd:1008::/64
- Wired/VPN Network for Teachers: 10.4.0.0/16, fd1e:2bae:c6fd:1004::/64

More info about the DEI's internal network can be found [here](https://rede.dei.isep.ipp.pt/usermanual/connect.html).

### 2.3. Research

To implement this feature, we need to use a tool that allows us to filter network traffic based on the source IP address. There are several tools that can be used to implement this feature, such as `iptables` and `nftables`.

#### 2.3.1 `iptables` vs `nftables`

**`iptables`** and **`nftables`** are tools that allow us to filter network traffic based on the source IP address. Both tools are widely used in Linux systems and have similar functionalities. 

`nftables` is the successor to `iptables` and is more modern and flexible, but due to the fact that `iptables` was the tool introduced to us in the ASIST Curricular Unit, it was decided to use `iptables` to implement this feature.

#### 2.3.1. What is `iptables`?

**Iptables** is used to set up, maintain, and inspect the tables of IP packet filter rules in the Linux kernel. Several different tables may be defined. Each table contains a number of built-in chains and may also contain user-defined chains.

Each chain is a list of rules which can match a set of packets. Each rule specifies what to do with a packet that matches. This is called a 'target', which may be a jump to a user-defined chain in the same table.

Print do manual do ipt

Relevant for this feature we have the following:

**Chains:**

INPUT - This chain is used for packets destined for the host computer.

**Targets:**

ACCEPT -> This means to let the packet through.

DROP -> This means to drop the packet on the floor.

**Commands:**

-A, --append -> Append one or more rules to the end of the selected chain.

-F, --flush -> Flush the selected chain (all the chains in the table if none is given).

-P, --policy -> Set the policy for the chain to the given target.

**Parameters:**

-s, --source -> Specify the source address or network.

-j, --jump -> Specify the target of the rule.

#### 2.3.2 The problem with `iptables`

The main problem with `iptables` is that it is not persistent. This means that if the system is restarted, the rules will be lost.

#### 2.3.3 Persisting the rules, `iptables-persistent` vs `netfilter-persistent`

To make the rules persistent, we can use the `iptables-persistent` package or the `netfilter-persistent` package. Both packages allow to save the current iptables rules and restore them at boot time in Debian-based distributions.

While both packages have similar functionalities, it was decided to use the `iptables-persistent` package to make the rules persistent, since we are already using the `iptables` tool to implement this feature, and the requirements of the feature are simple and can be met with the `iptables-persistent` package.

#### 2.3.3. What is `iptables-persistent`?

**Iptables-persistent** is a package that allows you to save the current iptables rules and restore them at boot time in Debian-based distributions.

### 2.4. Conclusion

To implement this feature, we will use the `iptables` tool to filter network traffic based on the source IP address. We will create a script that will contain the rules that will allow only users from the DEI internal network (wired or via VPN) to access the solution. We will then use the `iptables-persistent` package to make the rules persistent.

## 3. Implementation

### 3.1. Which VM to use?

To implement this feature, we will use a VM from DEI's private cloud. This VM will be used to test the implementation of the feature, due to the fact that it is connected to the DEI internal network and has a Public IP address, which will allow us to test the feature from outside the DEI internal network.

The VM from the ASIST PL classes will not be used to implement this feature, as it does not have a Public IP address, which will not allow us to test the feature from outside the DEI internal network.

#### 3.1.1. VM from DEI's private cloud

The VM from DEI's private cloud has the following characteristics:

- OS: Debian 11 Bullseye (Same as the ASIST PL classes VM)
- Global SSH/SFTP Access: vsgate-ssh.dei.isep.ipp.pt:10517
- DEI Internal Network Access: vs517.dei.isep.ipp.pt:22

Credentials:

- Username: root
- Password: Asistroot

For the implementation of this feature, the following steps will be taken:

### 3.2. Installation of `iptables`

First, we need to install the `iptables` package. To do this, run the following command:

```bash
sudo apt-get install iptables
```

### 3.3. Installation of `iptables-persistent`

Next, we need to install the `iptables-persistent` package.

```bash
sudo apt-get install iptables-persistent
```

### 3.4. Creation of the `iptables` rules

Next, we need to create the `iptables` rules that will allow only users from the DEI internal network (wired or via VPN) to access the solution. To do this, and to be able to edit the rules with ease, we will create a script that will contain the rules. The script can be found [here](./access_rules_ipv4.sh) and [here](./access_rules_ipv6.sh).

```bash
touch /etc/iptables/access_rules_ipv4.sh
touch /etc/iptables/access_rules_ipv6.sh
```
    
```bash
nano /etc/iptables/access_rules_ipv4.sh (Paste rules here)
nano /etc/iptables/access_rules_ipv6.sh (Paste rules here)
```

### 3.5. Making the script executable

After creating the script, we need to make it executable.

```bash
chmod +x access_rules_ipv4.sh
chmod +x access_rules_ipv6.sh
```

### 3.6. Running the script

Finally, we need to run the script to apply the rules.

```bash
./access_rules_ipv4.sh
./access_rules_ipv6.sh
```

### 3.7. Saving the rules

After running the script, the rules will be applied, but they will not be persistent. To make the rules persistent, we need to save them.

```bash
iptables-save > /etc/iptables/rules.v4
ip6tables-save > /etc/iptables/rules.v6
```

### 3.8. Check if the rules are persistent

To check if the rules are persistent, we need to restart the system and check if the rules are still applied.

```bash
reboot
```

After the system has restarted, we need to check if the rules are still applied.

```bash
iptables -L -v -n
```
(OUTPUT)

```bash
ip6tables -L -v -n
```
(OUTPUT)

### 3.9. Testing the feature

To test the feature, we will try to access the solution from a laptop connected to the DEI internal network and from a laptop connected to the internet.

#### 3.9.1. Access from the DEI internal network

To access the solution from the DEI internal network, we will use a laptop connected to the DEI internal network, via VPN and wired.

##### 3.9.1.1 Access from the DEI internal network (VPN)

(PRINT VPN CONNECTION)
(Print IP Address)

The solution was accessed successfully.

(PRINT)

##### 3.9.1.2 Access from the DEI internal network (wired)

(PRINT WIRED CONNECTION)
(PRINT IP Address)

The solution was accessed successfully.

(PRINT)

#### 3.9.2. Access from the internet

To access the solution from the internet, we will use a laptop connected to the internet.

(PRINT CONNECTION)
(PRINT IP Address)

The solution was not accessed successfully.

(PRINT)

## 4. Final Remarks

The implementation of this feature was successful. Only users from the DEI internal network (wired or via VPN) can access the solution. Users who are not on the DEI internal network (wired or via VPN) cannot access the solution.


