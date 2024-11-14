# US 647

## 1. Requirements

**US 6.4.7** Como administrador do sistema quero definir uma pasta pública para todos os utilizadores registados no sistema, onde podem ler tudo o que lá for colocado

**Acceptance Criteria:**

- 647.1 - Os utilizadores registados no sistema podem ler todos os ficheiros que se encontram na pasta pública
- 647.2 - A pasta pública deve ser configurada com permissões para ser acessível dentro da máquina


## 2. Analysis

### 2.1. Requirement

The system shall allow the administrator to define a public folder for all registered users in the system, where they can read everything that is placed there.

### 2.2. Research

After some research we found that there are multiple ways to implement this feature.

Here are some of the options we found:

- Local folder with read permissions for all users
- SMB/CIFS share configured with read permissions for all users
- NFS (Network File System) share configured with read permissions for all users
- FTP server with read permissions for all users
- Web server (HTTP) that allows the files to be read via a web browser

#### 2.2.1 Which options to use?

The options that we found are all valid and can be used to implement this feature. However, should we implement all of them? 

**Pros of using all of the options:**

- Maximum compatibility with different systems
- Maximum flexibility for the users
- Redundancy in case one or more options fail
- Users can choose the option that best suits their needs

**Cons of using all of the options:**

- Complexity in the implementation and maintenance
- Each service will consume resources from the system (CPU, RAM, disk space, etc.) which can lead to performance issues
- Security concerns, each service will expose an open port and protocol that can be a potential security risk

**Decision**

Answering the question above, we decided that we shouldn't implement all of the options. We will implement the option that we think is the best for our system and that will be the most useful for the users.

After this analysis we decided to implement a local folder with read permissions for all users. This option is the simplest to implement and maintain, meeting the requirements of the user story.

In addition to this, we will also implement Samba and NFS, as they are ideal for internal networks, especially in mixed environments with Windows and Linux machines. These technologies were also introduced to us in the classes.

#### 2.2.2 Linux permissions

In Linux, permissions are divided into three categories: owner, group, and others. Each category has three permissions: read, write, and execute.

- **Read:** Allows the file to be read
- **Write:** Allows the file to be modified
- **Execute:** Allows the file to be executed

The permissions are represented by numbers:

- **Read:** 4
- **Write:** 2
- **Execute:** 1

(PRINT SCREEN TP)

The permissions are added to form the desired permission. For example, if we want to give all the permissions to the owner and none to the group and others, we would use the number 700.

For this user story, we will use the permission 744 for the public folder. This means that the owner can read, write, and execute the folder, while the group and others can only read the folder.

#### 2.2.3 Samba

What is Samba? 

According to the University of Pensylvania, Samba is the standard Windows interoperability suite of programs for Linux and Unix, providing file and print services using the SMB/CIFS protocol

[University of Pennsylvania - Samba](https://cets.seas.upenn.edu/answers/samba.html)

#### 2.2.4 NFS

What is NFS?

According to IBM, the Network File System (NFS) is a mechanism for storing files on a network. It is a distributed file system that allows users to access files and directories located on remote computers and treat those files and directories as if they were local.

[IBM - NFS](https://www.ibm.com/docs/en/aix/7.1?topic=management-network-file-system)


## 3. Implementation

To implement this feature we will create a public folder in the system that will be accessible to all users. We will set the permissions of the folder to 755, which means that the users in the system can read and execute the folder, but only the owner can write to the folder.

There is a important detail related to the read and execute permissions. Despite the requirements of the user story being to read the files in the folder, the execute permission is also required to access the folder. This is because in Linux, to access a folder the user needs to have the execute permission on the folder.

We will also implement Samba and NFS to allow users to access the public folder from Windows and Linux machines.

### 3.1. Public folder

The public folder will be located at `/public`. We will create the folder and set the permissions to 744.

```bash
mkdir /public
chmod 755 /public
chown -R nobody:nogroup /public
```
Permissions of the folder:

(Print screen permissions)

#### 3.1.1. Testing the public folder

To test the public folder we will create a file in the folder:

```bash
echo "Hello, World!" > /public/hello.txt
```	

Next we will change to a user and try to read the file:

```bash
su asist
```

```bash
cat /public/hello.txt
```
(output of the file)

As we can see, the user `asist` can read the file in the public folder.

Now we will try to write to the file:

```bash
echo "Hello, World! Teste asist" > /public/hello.txt
```

As we can see, the user `asist` cannot write to the file in the public folder.


### 3.2. Samba

To implement Samba we will install the `samba` package and configure the `/etc/samba/smb.conf` file.

#### 3.2.1. Installation of Samba

First, we need to install the `samba` package.

```bash
apt install samba
```

#### 3.2.2. Configuration of Samba

Next, we need to configure the `/etc/samba/smb.conf` file.

```bash
cat /etc/samba/smb.conf
```

(Print screen smb.conf)

```bash
nano /etc/samba/smb.conf
```
We need to add the following lines to the file:

```bash
[public]
path = /public      
public = yes        
writable = no   
browsable = yes     
guest ok = no      
read only = yes
create mask = 0755
directory mask = 0755
```
Meaning of the parameters:

- path: Path to the public folder
- public: Public folder
- writable: Read-only
- browsable: Visible
- guest ok: No guest access
- read only: Read-only

#### 3.2.3. Restart Samba

After configuring the file, we need to restart the Samba service.

```bash
systemctl restart smbd
```
#### 3.2.4. Testing Samba

To test the Samba configuration we will use a Windows laptop to access the public folder.

(PRINTS)

### 3.3. NFS

To implement NFS we will install the `nfs-kernel-server` package and configure the `/etc/exports` file.

#### 3.3.1. Installation of NFS

First, we need to install the `nfs-kernel-server` package.

```bash
apt install nfs-kernel-server
```

#### 3.3.2. Configuration of NFS

Next, we need to configure the `/etc/exports` file.

```bash
cat /etc/exports
```

(Print screen exports)

```bash
nano /etc/exports
```

We need to add the following line to the file:

```bash
/public *(ro,sync,no_subtree_check,root_squash)
```

Meaning of the parameters:

- /public: Path to the public folder
- *: All machines
- ro: Read-only
- sync: Synchronous
- no_subtree_check: No subtree check
- root_squash: Root squash

#### 3.3.3. Restart NFS

After configuring the file, we need to restart the NFS service.

```bash
systemctl restart nfs-kernel-server
```

#### 3.3.4. Testing NFS

To test the NFS configuration we will use a Linux machine to access the public folder.

We will use a VM from DEI's private cloud to test the NFS configuration.

VM Details:

- IP: 10.9.25.147
- Username: root
- Password: Asistroot

First, we need to install the `nfs-common` package.

```bash
apt install nfs-common
```

Next, we need to create a folder to mount the NFS share.

```bash
mkdir /mnt/public
```

Next, we need to mount the NFS share.

```bash
mount 10.9.10.43:/public /mnt/public
```

Next, we need to list the files in the folder.

```bash
ls /mnt/public
```
```bash
cat hello.txt
```

(Print screen files)

As we can see, the files in the public folder are listed.

We can try to write to the file, to test the read-only permissions.

```bash
echo "Hello, World! Teste NFS" > /mnt/public/hello.txt
```

As we can see, the user cannot write to the file in the public folder.

(Print screen error)

### 3.4. Final considerations

In this user story we implemented a public folder that is accessible to all users in the system. We also implemented Samba and NFS to allow users to access the public folder from Windows and Linux machines.

