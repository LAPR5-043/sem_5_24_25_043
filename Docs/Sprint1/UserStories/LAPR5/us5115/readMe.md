# 5.1.15 As an Admin, I want to list/search staff profiles, so that I can see the details, edit, and remove staff profiles.

## 1. Context

We want to list/search staff applying some filters, including some sort criteria.
## 2. Requirements


**US 5.1.15** 

**Acceptance Criteria:**

- Admins can search staff profiles by attributes such as name, email, or specialization.
- The system displays search results in a list view with key staff information (name, email, specialization).
- Admins can select a profile from the list to view, edit, or deactivate.
- The search results are paginated, and filters are available for refining the search results.

**Dependencies/References:**

Regarding this requirement we understand that it relates with user story 5.1.14, because it will be used to deactivate some staff in the us 5.1.14.

## 3. Analysis

![analyzis ](analyzis\png\analyzis.svg "analyzis")

## 4. Design


### Sequence Diagram

![desing ](design\png\sequence-diagram.svg "desing")

The X on some diagram lines is one of the choosen attributes.



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