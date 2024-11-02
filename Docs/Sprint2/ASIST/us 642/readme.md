# US 6.4.2

## 1. Requirements

**US 6.4.2** Como administrador do sistema quero que apenas os clientes da rede interna do DEI (cablada ou via VPN) possam aceder à solução

**Acceptance Criteria:**

- 642.1 - Os utilizadores que se encontrem na rede interna do DEI (cablada ou via VPN) podem aceder à solução

- 642.2 - Os utilizadores que não se encontrem na rede interna do DEI (cablada ou via VPN) não podem aceder à solução


## 2. Analysis

### 2.1. Requirement

The system shall only allow users from the DEI internal network (wired or via VPN) to access the solution. Users who are not on the DEI internal network (wired or via VPN) should not be able to access the solution.

### 2.2. Research

After some research, it was found that the best way to implement this feature is by using the **`iptables`** tool.

#### 2.2.1. What is `iptables`?

**Iptables** is used to set up, maintain, and inspect the tables of IP packet filter rules in the Linux kernel. Several different tables may be defined. Each table contains a number of built-in chains and may also contain user-defined chains.

Each chain is a list of rules which can match a set of packets. Each rule specifies what to do with a packet that matches. This is called a 'target', which may be a jump to a user-defined chain in the same table.

Print do manual do ipt

#### 2.2.2 The problem with `iptables`

The main problem with `iptables` is that it is not persistent. This means that if the system is restarted, the rules will be lost. To solve this problem, we can use the `iptables-persistent` package.

#### 2.2.3. What is `iptables-persistent`?

**Iptables-persistent** is a package that allows you to save the current iptables rules and restore them at boot time in Debian-based distributions.

### 2.3. Decision

Based on the requirement that only users from the DEI internal network (wired or via VPN) should be able to access the solution, it was decided to use the **`iptables`** tool to implement this feature. The **`iptables-persistent`** package will also be used to ensure that the rules are persistent.

## 3. Implementation

For the implementation of this feature, the following steps will be taken:

### 3.1. Installation of `iptables-persistent`

First, we need to install the `iptables-persistent` package. To do this, run the following command:

```bash
sudo apt-get install iptables-persistent
```

### 3.2. Creation of the `iptables` rules

Next, we need to create the `iptables` rules that will allow only users from the DEI internal network (wired or via VPN) to access the solution. To do this, and to be able to edit the rules with ease, we will create a script that will contain the rules. The script can be found [here](./access_rules.sh).

### 3.3. Making the script executable

After creating the script, we need to make it executable.

```bash
chmod +x access_rules.sh
```

### 3.4. Running the script

Finally, we need to run the script to apply the rules.

```bash
sudo ./access_rules.sh
```

### 3.5. Saving the rules

After running the script, the rules will be applied, but they will not be persistent. To make the rules persistent, we need to save them.

```bash
sudo iptables-save > /etc/iptables/rules.v4
```

### 3.6. Check if the rules are persistent

To check if the rules are persistent, we need to restart the system and check if the rules are still applied.

```bash
sudo reboot
```

After the system has restarted, we need to check if the rules are still applied.

```bash
sudo iptables -L
```
(OUTPUT)