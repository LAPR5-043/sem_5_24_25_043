# 5.1.14 As an Admin, I want to deactivate a staff profile, so that I can remove them from the hospital’s active roster without losing their historical data. 

## 1. Context

We want to deactivate a staff from the system
## 2. Requirements


**US 5.1.14** 

**Acceptance Criteria:**

- Admins can search for and select a staff profile to deactivate.
- Deactivating a staff profile removes them from the active roster, but their historical data (e.g., appointments) remains accessible.
- The system confirms deactivation and records the action for audit purposes.

**Dependencies/References:**

Regarding this requirement we understand that it relates with user story 5.1.15, because it will be used in the staff which appears on the list showed by the us 5.1.15, so we don't need to use DTOS because nothing new will be shown to the user 


## 3. Analysis

![analyzis ](analyzis\png\analyzis.svg "analyzis")

We also will need to regist the deactivation, so we'll use Logs

![logs ](analyzis\png\logs.svg "logs")

## 4. Design


### Sequence Diagram

![desing ](design\png\sequence-diagram.svg "desing")


