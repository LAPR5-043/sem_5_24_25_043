# Sensitive Data Management

As some information, (e.g name, email, phone number), is sensitive, we need to find a way to protect this data. 

The best solution for this, that we have found, is to have a configuration file where the respective types of data are persisted, and if we need to know if an attribute is consider sensitive, a service will check the file and search for the attribute. 

This way, we have a dynamic list which can have multiple types of sensitive data added in the future


This process is represented in the following diagram

![sensistive data](diagrams\png\sensitiveDataService.svg "sensitiveData")


## Pending Request

After the service classify the attribute has sensitive data, the controller or service, which called the SensisitveDataService, will create a PendingRequest, where it will persist the data supposed to be change.


![pendingrequest](diagrams\png\PendingRequest.svg "pendingrequest")


### Pending Request Creation

![pendingrequest](diagrams\png\pendingRequestService.svg "pendingrequest")

The after accept the email process will be different depending what user story we're talking about

