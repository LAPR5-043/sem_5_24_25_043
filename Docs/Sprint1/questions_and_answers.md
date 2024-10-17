
# Questions answered

## Our questions:

**Q: Can a user have more than one role? (e.g., be a doctor and a patient, at the same time)?**  
**A:** No, a user cannot have both roles. Staff and patients have separate identifications.

**Q: Do nurses have specializations like doctors?**  
**A:** Yes, nurses can have specializations, which are important for specific surgeries.

**Q: Will there be a list of specializations in the system?**  
**A:** Yes, a predefined list of specializations will be provided, but the system should allow for future additions.

---

## Other important questions:

**Q: How should the capacity of a surgery room be interpreted?**  
**A:** The capacity refers to the number of staff in the room, not patients.

**Q: How are duplicate patient profiles handled when registered by both the patient and admin?**  
**A:** The system checks the email for uniqueness. The admin must first create the patient record, and then the patient can register using the same email (a patient can't self-register without having a patient record created).

**Q: What factors should be considered for surgery scheduling?**  
**A:** A combination of professional seniority, surgery duration, and urgency will be considered. Scheduling will occur in the second sprint.

**Q: Is it mandatory for patients to have a user account to schedule a surgery?**  
**A:** No, patients are not required to have a user account. The system administrator creates patient profiles.

**Q: Are healthcare staff IDs unique across roles?**  
**A:** Yes, staff IDs are unique and not role-specific (e.g., a doctor and nurse can share the same ID format).

**Q: What is the difference between appointment, surgery, and operation?**  
**A:** Surgery is a medical procedure (e.g., hip surgery), while an operation request is when a doctor schedules that surgery for a patient. An appointment is the scheduled date for the operation, determined by the planning module.

**Q: Can surgeries be rescheduled?**  
**A:** Yes, surgeries can be rescheduled due to various reasons like emergencies or changes in staff availability.

**Q: Who has the authority to schedule or reschedule surgeries?**  
**A:** The planning module automatically handles the scheduling, though administrators may trigger a manual update.

**Q: What happens to patient data after the profile is deleted?**  
**A:** Patient data must be retained for a legally mandated period before being anonymized or deleted.

**Q: Can the same doctor who requests a surgery perform it?**  
**A:** Not necessarily. The planning module may assign different doctors based on availability and optimization.

**Q: The 'operation request' has the 'priority' attribute. What priorities are there?**  
**A:** Elective, Urgent, and Emergency Surgery Classifications.
- Elective Surgery: A planned procedure that is not life-threatening and can be scheduled at a convenient time (e.g., joint replacement, cataract surgery).
- Urgent Surgery: Needs to be done sooner but is not an immediate emergency. Typically, within days (e.g., certain types of cancer surgeries).
- Emergency Surgery: Needs immediate intervention to save life, limb, or function. Typically performed within hours (e.g., ruptured aneurysm, trauma).

**Q: In the staff's 'Availability Slots', will the "temporal gap" be every hour, customizable, or another option?**  
**A:** The staff's availability is usually in 15-minute blocks

**Q: For the appointment's 'Date and Time' attribute, is there a specific format?**  
**A:** YYYY/MM/DD HH:MM

**Q: What are the system's password requirements?**  
**A:** at least 10 characters long, at least a digit, a capital letter and a special character.

**Q: What types of filters can be applied when searching for profiles?**  
**A**: Filters can include doctor specialization, name, or email to refine search results.
Users should be able to search patients by: name, AND/OR email, AND/OR phone number, AND/OR medical record number, AND/OR date of birth, AND/OR gender.  
Listing of users should have the same filters available.

**Q: It is specified that the admin can input some of the patient's information (name, date of birth, contact information, and medical history).  
Do they also input the omitted information (gender, emergency contact and allergies/medical condition)?  
Additionally, does the medical history that the admin inputs refer to the patient's medical record, or is it referring to the appointment history?**  
**A**: The admin cannot input medical history nor allergies. They can however input gender and emergency contact.

**Q: When listing operation requests, should only the operation requests associated to the logged-in doctor be displayed?**  
**A:** A doctor can see the operation requests they have submitted as well as the operation requests of a certain patient.  
An Admin will be able to list all operation requests and filter by doctor.  
It should be possible to filter by date of request, priority and expected due date.

**Q: How should the specialization be assigned to a staff?
Should the admin write it like a first name? Or should the admin select the specialization?**  
**A:** The system has a list of specializations. Staff is assigned a specialization from that list.

**Q: Regarding the creation of staff users and profiles, there are 2 separate use cases regarding backoffice users: One for the creation of the user account and another one for the creation of the staff's profile.**

- **Is there a fixed order for these operations to take place? Does the admin always create the profile first or can he create the user first aswell?**

- **If the profile is created first, for example, should the user be created automaticaly or should the admin create the user afterwards, having to do 2 distinct operations?**
  
**A:** Recommended Flow:  
_Order of operations_: The system should support profile first. The admin should then create the user account. The account and user profile are linked by the professional email address or username (depending on the IAM provider).

_Distinct Operations_: The operations should remain distinct, even if they are performed in quick succession. This ensures that each step (creating user credentials and creating a staff profile) is carefully tracked and managed.

_Validation_: The system should ensure that a staff profile and user account are both created and linked before the staff member can access the system. 

