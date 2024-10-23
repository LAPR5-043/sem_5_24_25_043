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

We already know the Operation Type id, so we don't need to use DTOS because nothing new will be shown to the user 

## 3. Analysis

![analyzis ](analyzis\png\analyzis.svg "analyzis")

## 4. Design


### Sequence Diagram

![desing ](design\png\sequence-diagram.svg "desing")




