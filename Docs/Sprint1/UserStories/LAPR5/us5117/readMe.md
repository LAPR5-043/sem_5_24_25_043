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

This user story depends of US 5.1.19, because doctor needs to list all the operation requisitions before select one, we already know which is the operationRequestID before the us start, so we don't need to use DTOS because nothing new will be shown to the user 

## 3. Analysis

![analyzis ](analyzis\png\analyzis.svg "analyzis")

We also will need to regist the operation request changes, so we'll use Logs

![logs ](analyzis\png\logs.svg "logs")

## 4. Design


### Sequence Diagram

![desing ](design\png\sequence-diagram.svg "desing")




