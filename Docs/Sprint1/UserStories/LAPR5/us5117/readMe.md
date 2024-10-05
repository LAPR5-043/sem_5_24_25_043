# 5.1.17 As a Doctor, I want to update an operation requisition, so that the Patient has access to the necessary healthcare

## 1. Context

We want to edit a operation request, to change the deadline or priority, for exemaple
## 2. Requirements


**US 5.1.17** 

**Acceptance Criteria:**

- Doctors can update operation requests they created (e.g., change the deadline or priority).
- The system checks that only the requesting doctor can update the operation request.
- The system logs all updates to the operation request (e.g., changes to priority or deadline).
- Updated requests are reflected immediately in the system and notify the Planning Module of any changes.

**Dependencies/References:**

This user story depends of US 5.1.19, because doctor needs to list all the operation requisitions before select one

## 3. Analysis

![analyzis ](analyzis\png\analyzis.svg "analyzis")

## 4. Design


### Sequence Diagram

![desing ](design\png\sequence-diagram.svg "desing")




@Test(expected = IllegalArgumentException.class)
public void ensureXxxxYyyy() {
    ...
}
````

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