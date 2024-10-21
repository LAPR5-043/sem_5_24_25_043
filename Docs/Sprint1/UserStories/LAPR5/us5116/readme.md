# US 5.1.16

## 1. Context

*This task was assigned in Sprint 1. <br>
Its purpose is for a Doctor to be able to request an operation for a Patient.*
## 2. Requirements

### 2.1. Acceptance Criteria
- Doctors can create an operation request by selecting the patient, operation type, priority, and suggested deadline.
- The system validates that the **operation type** matches the doctor’s specialization.
- The operation request includes:
- Patient ID
- Doctor ID
- Operation Type
- Deadline
- Priority
- The system confirms successful submission of the operation request and logs the request in the patient’s medical history.

### 2.2. Description & Dependencies
**US 5.1.16** - As a Doctor, I want to request an operation, so that the Patient has access to the necessary healthcare.

We can find dependencies with the following functional requirements:
- **US 5.1.1** - Backoffice users need to be registered in the system in order to request an operation.
  - **US 5.1.12** - A Staff Member's profile must be created by an Admin before being able to be registered. 
- **US 5.1.3** - Patients need to be registered in the system in order to be able to have an operation request.
  - **US 5.1.8** - A Patient's profile must be created by an Admin before they can register. 


### 2.2 Client Specifications

> - **Question:** <br>
    ". . ."
    <br><br>
> - **Answer:** <br>
    ". . .."


## 3. Analysis
An Admin . . .
***TO-DO***
Verificar como funciona a operation request. Em termos de especialização, Operation Type, Surgery Room e Equipment, Pending Request...

### 3.1 Domain Model excerpt
![US5116_DM](analysis/png/US5116_DM.svg "US 5.1.16 Domain Model")


### 3.2 Use case diagram
![US5116_UCD](analysis/png/US5116_UCD.svg "US 5.1.16 Use Case Diagram")

## 4. Design

#### 4.1 Sequence Diagram
![US5116_SD](SD/US5116_SD.svg "US 5.1.16 Sequence Diagram")


<br>

## 5. Implementation

### ExampleController

The Controller implements the methods:
- *exampleMethod* - Explain what the method does;
- *exampleMethod2* - Explain what the method does;
```


```


**Major commits:**

* [Added code for . . .](https://github.com/...)


## 6. Integration/Demonstration

To be able to list exam grades, course must be created, an exam must be created and associated to said course, and a student take said exam.


The method *exampleMethod* of the class *exampleService* was already implemented before the development of this functional requirement.

<br>

### User interaction demonstration

<details>
  <summary> UI example when . . . </summary>

![Example_UI](DemonstrationExamples/example_ui.png "An example of what happens when . . .")
</details>


## 7. Observations
The reset of the password . . . <br>