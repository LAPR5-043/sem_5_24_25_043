# Surgical Appointment and Resource Management System

## Project Overview
This project is aimed at developing a **web-based prototype** system for managing **surgical appointments and hospital resources**. The system helps hospitals and clinics schedule surgeries efficiently, manage patient and staff data, and optimize resource usage while ensuring compliance with GDPR (General Data Protection Regulation). It also incorporates real-time 3D visualization of resource availability and supports business continuity.

## Features
- **Surgery Scheduling**: Manage and optimize surgery appointments based on staff, room, and equipment availability.
- **3D Resource Visualization**: Real-time 3D display of hospital rooms and resources.
- **Back-office Management**: Administer patient records, medical staff, operation requests, and resources.
- **GDPR Compliance**: Ensure data protection, with features like patient consent management and the right to be forgotten.
- **Business Continuity**: Failover mechanisms to ensure system availability during disruptions.

## Project Modules
1. **Back-office Web Application**: Manages users (staff and patients), operation requests, and hospital rooms.
2. **3D Visualization Module**: Displays the hospital’s rooms and their current usage in real-time.
3. **Planning and Optimization Module**: Schedules and optimizes surgeries based on resource constraints.
4. **GDPR Compliance Module**: Ensures all operations adhere to GDPR guidelines for data protection and consent management.
5. **Business Continuity Plan (BCP)**: Provides measures to ensure system functionality during failures.

## Technologies Used
- **Web Technologies**: Full-stack development with REST APIs.
- **Database Management**: To store and manage users, appointments, and operation requests.
- **3D Rendering**: For real-time visualization of hospital resources.
- **Prolog**: To implement planning and optimization algorithms.
- **CI/CD Pipeline**: For continuous integration and deployment.

## System Components
### 1. **User Management**
   - **Entities**: Admin, Doctors, Nurses, Technicians, Patients.
   - **Attributes**: Username, Role, Email, First Name, Last Name, Medical Record Number, Specialization, etc.
   
### 2. **Appointment Management**
   - **Entities**: Surgery Appointments, Operation Requests, Surgery Rooms.
   - **Attributes**: ID, Room Number, Date/Time, Status, Patient/Doctor details, Operation Type, etc.

### 3. **GDPR Compliance**
   - **Features**: Patient data privacy, right to be forgotten, and consent management.
   - **Data Protection**: All personal and sensitive data are securely handled following GDPR requirements.

## Development Workflow
### Project Phases (14 weeks, 3 sprints)
1. **Sprint 1**:
   - Focus on back-office functionality: User registration, appointment booking, and management.
2. **Sprint 2**:
   - Integration of 3D visualization, GDPR module, and planning module for surgery scheduling.
3. **Sprint 3**:
   - Finalize business continuity, testing, and system integration.

### Agile Practices
- **Daily Standups/Weekly Meetings**: Teams meet regularly to track progress and resolve dependencies.
- **Code Reviews**: Regular peer code reviews to ensure high-quality code.
- **Customer Feedback**: Present progress to the customer (professor) after each sprint for feedback.
- **Continuous Integration/Deployment (CI/CD)**: Automatic testing and deployment pipelines.

## License
This project is licensed under the MIT License.
