# US 5.1.2

## 1. Context

*This task was assigned in Sprint 1. <br>
Its purpose is for a backoffice user to be able to reset their password.*
## 2. Requirements

### 2.1 Description & Dependencies
**US 5.1.2** - As a Backoffice User (Admin, Doctor, Nurse, Technician), I want to reset my password if I forget it, so that I can regain access to the system securely.

*Regarding this requirement we understand that it relates to US . . .*
* We can find dependencies with the following functional requirements:
    - *FRE04* (US2004) - Students need to take exams in order for exams to have grades;

### 2.2 Client Specifications

> - **Question:** <br>
    ". . . "
    <br><br>
> - **Answer:** <br>
    ". . ."

> - **Question:** <br>
    ". . ."
    <br><br>
> - **Answer:** <br>
    ". . ."

### 2.3 Input and Output data
- *Input data:* </br>
    - Option of course(s) to display the grades of

- *Output data:*
    - List of option of courses the teacher is apart of
    - List of grades of the students of the course(s) selected



## 3. Analysis
A Teacher is responsible for creating exams. <br>
A Student takes an exam and answers its questions. <br>
A Teacher is able to view the grades of exams, for courses they are a part of. <br>
The list of grades of the exams a teacher views is only for general exams, grades of formative exams aren't stored, and as such shouldn't be displayed. <br>
The teacher should be able to specify from which courses the grades displayed are a part of. They should be able to view the grades of all the courses, if desired.   <br>
The grades should be separated by course, which are in turn sorted by exam. Each grade displayed should be accompanied by their respective student.

### 3.1 Domain Model excerpt
![US2006_DM](DM_excerpt/US2006_DM.svg "US2006 Domain Model")

### 3.2 Use case diagram
![US2006_UCD](UCD/US2006_UCD.svg "US2006 Use Case Diagram")

## 4. Design
#### 4.1.1 System Sequence Diagram
![US2006_SSD](SSD/US2006_SSD.svg "US2006 System Sequence Diagram")

#### 4.1.2 Sequence Diagram
![US2006_SD](SD/US2006_SD.svg "US2006 Sequence Diagram")

### 4.2. Class Diagram
![US2006_CD](CD/US2006_CD.svg "US2006 Class Diagram")

### 4.3. Applied Patterns

| Interaction ID | Question: Which class is responsible for... | Answer                      | Justification (with patterns)                                                                                                                                |
|:---------------|---------------------------------------------|-----------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Step 1         | ... interacting with the actor?             | ListGradesOfExamsUI         | Pure Fabrication: There is no reason to assign this responsibility to any existing class in the Domain Model.                                                |
| Step 2         | ... coordinating the US?                    | ListGradesOfExamsController | Controller: The controller creates instances of other classes to handle the request and coordinate the flow of control.                                      |




### 4.4. Tests

**Test 1:** *Verifies the courses of the user can be found*
```
    @Test

```

<br>

## 5. Implementation

### ListGradesOfExamsController

The Controller implements the methods:
- *findCoursesOfUser* - Groups all the courses the user is a part of, to list them;
- *findGradesOfExam* - Groups all exams taken by students, to list them;
```


```


**Major commits:**

* [Added code for functional requirement and tests](https://github.com/Departamento-de-Engenharia-Informatica/sem4pi-22-23-30/commit/f8cb1ba6ccf7c14d8a1c4ce8de8491ddf0401232)
* [Added UI for Teacher](https://github.com/Departamento-de-Engenharia-Informatica/sem4pi-22-23-30/commit/368c111bde4d51cb38034fbfd6975bd0fcd95fbc)
* [Added completed readme](https://github.com/Departamento-de-Engenharia-Informatica/sem4pi-22-23-30/commit/8675df5cfb2b8ce2914c08ecd7121147b0b27131)


## 6. Integration/Demonstration

To be able to list exam grades, course must be created, an exam must be created and associated to said course, and a student take said exam <br>


The method *findCourseByCode* of the class *CourseManagementService* was already implemented before the development of this functional requirement. <br>
The method *findTeachingStaffByTeacher* of the class *TeachingStaffService* was already implemented before the development of this functional requirement. <br>
The method *findTeacherByEmail* of the class *TeacherService* was already implemented before the development of this functional requirement. <br>
The method *getAllExamsOfACourse* of the class *ExamService* was already implemented before the development of this functional requirement. <br>
The method *course* of the class *TeachingStaff* was already implemented before the development of this functional requirement. <br>
The method *toDTO* of the class *Course* was already implemented before the development of this functional requirement. <br>

<br>

### User interaction demonstration

<details>
  <summary> UI example when the user isn't in any course: </summary>

![Example_NoCoursesFound](DemonstrationExamples/NoCoursesFound.png "An example of what happens when there are no courses")
</details>

<br>

<details>
  <summary> UI example when the user asks for the grades of a specific course: </summary>

![Example_GradesSpecificCourse](DemonstrationExamples/GradesSpecificCourse.png "An example of what happens when the user chooses a specific course")
</details>

<br>

<details>
  <summary> UI example when the user asks for the grades of all courses: </summary>

![Example_GradesAllCourses](DemonstrationExamples/GradesAllCourses.png "An example of what happens when the user chooses all courses")
</details>

<br>

## 7. Observations
The course of the grades to be listed will always be displayed, regardless if the user has chosen to see the grades of a specific course, or all of them. <br>