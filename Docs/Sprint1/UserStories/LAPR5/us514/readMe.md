# US 5.1.4 As a Patient, I want to update my user profile, so that I can change my personal details and preferences.


## 1. Context

We want to give the user the opportunity of change his personal data 
## 2. Requirements


**US 5.1.4** 

**Acceptance Criteria:**

- Patients can log in and update their profile details (e.g., name, contact information, preferences).

- Changes to sensitive data, such as email, trigger an additional verification step (e.g., confirmation email).

- All profile updates are securely stored in the system.

- The system logs all changes made to the patient's profile for audit purposes.

**Dependencies/References:**

Regarding this requirement we understand that it doesn't relates to any user story.

## 3. Analysis

![analyzis ](analyzis\png\analyzis.svg "analyzis")



![request ](analyzis\png\PendingRequest.svg "request")
## 4. Design


### 4.1. Attribute Change

![part1 ](design\png\sequence-diagram-part1.svg "part1")

After this, the non-sensitive attributes were changed, and the sensitive ones, are present in a pendingRequest, waiting for the patient accept the change in the email.


### 4.2 Change Confirmation's
Here, we will represent what happens right after the patient accept the change in the email, which contains a link to the api which will call the respective controller

![part2 ](design\png\sequence-diagram-part2.svg "part2")







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