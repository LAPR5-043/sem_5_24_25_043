# 5.1.10 As an Admin, I want to delete a patient profile, so that I can remove patients who are no longer under care

## 1. Context

We need to delete a patient account from the system.

## 2. Requirements


**Acceptance Criteria:**

- Admins can search for a patient profile and mark it for deletion.
- Before deletion, the system prompts the admin to confirm the action.
- Once deleted, all patient data is permanently removed from the system within a predefined time frame.
- The system logs the deletion for audit and GDPR compliance purposes.

**Dependencies/References:**

Regarding this requirement we understand that it relates to the user story 5.1.11 because you need to search/list the patient in order to delete it, so we know the patient id before the user story even have started, so we don't need to use DTOS because nothing new will be shown to the user 


## 3. Analysis

![analyzis ](analyzis\png\analyzis.svg "analyzis")

![logs ](analyzis\png\logs.svg "logs")

## 4. Design

![design ](design\png\sequence-diagram.svg "design")



## 5. Implementation

*In this section the team should present, if necessary, some evidencies that the implementation is according to the design. It should also describe and explain other important artifacts necessary to fully understand the implementation like, for instance, configuration files.*

*It is also a best practice to include a listing (with a brief summary) of the major commits regarding this requirement.*

## 6. Integration/Demonstration

*In this section the team should describe the efforts realized in order to integrate this functionality with the other parts/components of the system*

*It is also important to explain any scripts or instructions required to execute an demonstrate this functionality*

## 7. Observations

*This section should be used to include any content that does not fit any of the previous sections.*

*The team should present here, for instance, a critical prespective on the developed work including the analysis of alternative solutioons or related works*

*The team should include in this section statements/references regarding third party works that were used in the development this work.*