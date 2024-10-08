# Sensitive Data Management

As some information, (e.g name, email, phone number), is sensitive, we need to find a way to protect this data. 

The best solution for this, that we have found, is to have a configuration file where the respective types of data are persisted, and if we need to know if an attribute is consider sensitive, a service will check the file and search for the attribute. 

This way, we have a dynamic list which can have multiple types of sensitive data added in the future


This process is represented in the following diagram

![sensistive data](diagrams\png\sensitiveDataService.svg "sensitiveData")