
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
