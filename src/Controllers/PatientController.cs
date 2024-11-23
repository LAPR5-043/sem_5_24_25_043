using Domain.PatientAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sem_5_24_25_043;
using src.Services.IServices;
using src.Services.Services;

namespace src.Controllers
{
    /// <summary>
    /// Patient controller
    /// </summary>
    [Route("api/[controller]")]
    //[Authorize(Roles = "admins, patient")]
    [ApiController]

    public class PatientController : ControllerBase
    {
        /// <summary>
        /// Patient service
        /// </summary>
        private readonly IPatientService service;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public PatientController(IPatientService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Create a new patient
        /// </summary>
        /// <param name="patientDto"></param>
        /// <returns></returns>
        // POST: api/Patient/Create
        [Authorize(Roles = "admins")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePatient([FromBody] PatientDto patientDto)
        {


            if (patientDto == null)
            {
                return BadRequest(new { message = "Invalid patient data." });
            }

            try
            {
                var createdPatient = await service.CreatePatientAsync(patientDto);
                if (createdPatient != null)
                {
                    return CreatedAtAction(nameof(GetPatientById), new { id = createdPatient.MedicalRecordNumber }, createdPatient);
                }

                return StatusCode(500, new { message = "An error occurred while creating the patient." });
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }


        //GET /api/Patient/filtered?firstName=&lastName=&email=&phoneNumber=&medicalRecordNumber=&dateOfBirth=&gender=&sortBy=
        [Authorize(Roles = "admins")]
        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatientsFiltered([FromQuery] string? firstName, [FromQuery] string? lastName,
                                                                    [FromQuery] string? email, [FromQuery] string? phoneNumber, [FromQuery] string? medicalRecordNumber, [FromQuery] string? dateOfBirth, [FromQuery] string? gender, [FromQuery] string? sortBy)
        {
            var patient = await service.GetPatientsFilteredAsync(firstName, lastName, email, phoneNumber, medicalRecordNumber, dateOfBirth, gender, sortBy);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // GET: api/Patient/5
        [Authorize(Roles = "admins")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(string id)
        {
            var patient = await service.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound(new { message = "Patient not found." });

            }

            return Ok(patient);
        }

        [Authorize(Roles = "admins")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            var adminEmail = User.Claims.First(claim => claim.Type == "custom:internalEmail").Value;

            var result = await service.DeletePatientAsync(id, adminEmail);
            if (result)
            {
                return Ok(new { message = "Patient deleted successfully." });
            }
            return NotFound(new { message = "Patient not found." });
        }

        [Authorize(Roles = "patient")]
        [HttpPut("personalData/{id}")]
        public async Task<IActionResult> UpdatePatient(string id, [FromBody] PatientDto patientDto)
        {

            if (patientDto == null)
            {
                return BadRequest(new { message = "Invalid patient data." });
            }

            var patientExists = await service.GetPatientByIdAsync(id);
            if (patientExists == null)
            {
                return NotFound(new { message = "Patient not found." });
            }

            var result = await service.UpdatePatientAsync(id, patientDto);

            if (result)
            {
                return Ok(new { message = "Patient updated successfully." });
            }


            return NotFound(new { message = "Patient not found." });
        }

        [HttpGet("acceptPendingRequests")]
        public async Task<IActionResult> AcceptPatientPendingRequests([FromQuery] string requestIds)
        {
            if (requestIds == null)
            {
                return BadRequest(new { message = "Invalid pending requests data." });
            }


            List<long> requests = new List<long>();

            foreach (var requestId in requestIds.Split(','))
            {
                if (long.TryParse(requestId, out long id))
                {
                    requests.Add(id);
                }
            }

            var accepted = service.AcceptRequests(requests).Result;
            if (accepted)
            {
                return Ok(new { message = "pending requests accepted successfully." });
            }

            return NotFound(new { message = "pending requests not found." });
        }

        [HttpPatch("edit/{id}")]
        public async Task<IActionResult> EditPatient(string id, [FromBody] PatientDto patientDto)
        {
            if (patientDto == null)
            {
                return BadRequest(new { message = "Invalid patient data." });
            }

            var adminEmail = User.Claims.First(claim => claim.Type == "custom:internalEmail").Value;
            var patientExists = await service.GetPatientByIdAsync(id);
            if (patientExists == null)
            {
                return NotFound(new { message = "Patient not found." });
            }

            var patient = await service.EditPatientAsync(id, patientDto, adminEmail);

            if (patient != null)
            {
                var response = new
                {
                    MedicalRecordNumber = patient.MedicalRecordNumber.medicalRecordNumber,
                    FirstName = patient.FirstName.ToString(),
                    LastName = patient.LastName.ToString(),
                    FullName = patient.FullName.ToString(),
                    Email = patient.Email.ToString(),
                    PhoneNumber = patient.PhoneNumber.ToString(),
                    EmergencyContactName = patient.EmergencyContact?.Name ?? "",
                    EmergencyContactPhoneNumber = patient.EmergencyContact?.PhoneNumber ?? "",
                    DayOfBirth = patient.DateOfBirth?.Day().ToString(),
                    MonthOfBirth = patient.DateOfBirth?.Month(),
                    YearOfBirth = patient.DateOfBirth?.Year(),
                    Gender = patient.Gender == 0 ? "Male" : "Female",
                    AllergiesAndConditions = patient.AllergiesAndConditions,
                    AppointmentHistory = patient.AppointmentHistory
                };

                return CreatedAtAction(nameof(GetPatientById), new { id = patient.MedicalRecordNumber }, response);
            }
           


            return NotFound(new { message = "Patient not found." });
            
        }

        // DELETE: /api/patient/delete/personalAccount?confirmDeletion=
        [Authorize(Roles = "patient")]
        [HttpDelete("delete/personalAccount")]
        public async Task<IActionResult> DeletePersonalAccount([FromQuery] bool? confirmDeletion)
        {
            var patientEmail = User.Claims.First(claim => claim.Type == "custom:internalEmail").Value;

            try
            {
                var result = await service.DeletePersonalAccountAsync(patientEmail, confirmDeletion);
                if (result)
                {
                    return Ok(new { message = "Account deletion request successful" });
                }
                return NotFound(new { message = "Patient personal account not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: /api/patient/delete/sensitiveData?patientID=
        //[Authorize(Roles = "patient")]
        [HttpGet("delete/sensitiveData")]
        public async Task<ActionResult<string>> DeleteSensitiveData([FromQuery] string patientID)
        {
            try
            {
                var deleted = await service.DeleteSensitiveDataAsync(patientID);
                if (deleted)
                {
                    return Ok(new { message = "Patient account deletion confirmed." });
                }
                return NotFound(new { message = "Failed account deletion." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}