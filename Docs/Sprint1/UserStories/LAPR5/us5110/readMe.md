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



