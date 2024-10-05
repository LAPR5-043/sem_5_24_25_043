# 5.1.22 As an Admin, I want to remove obsolete or no longer performed operation types, so that the system stays current with hospital practices. 

## 1. Context

We want to delete or deactivate a operation type from the system
## 2. Requirements


**US 5.1.22** 

**Acceptance Criteria:**

- Admins can search for and mark operation types as inactive (rather than deleting them) to preserve historical records.
- Inactive operation types are no longer available for future scheduling but remain in historical data.
- A confirmation prompt is shown before deactivating an operation type.

**Dependencies/References:**

N/A

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