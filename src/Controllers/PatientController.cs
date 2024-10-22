using Domain.PatientAggregate;
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
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePatient([FromBody] PatientDto patientDto)
        {
            if (patientDto == null)
            {
                return BadRequest(new { message = "Invalid patient data." });
            }

            var createdPatient = await service.CreatePatientAsync(patientDto);
            if (createdPatient != null)
            {
                return CreatedAtAction(nameof(GetPatientById), new { id = createdPatient.MedicalRecordNumber }, createdPatient);
            }

            return StatusCode(500, new { message = "An error occurred while creating the patient." });
        }

        [HttpPost("signin-patient")]
        public async Task<ActionResult<string>> SignInPatientAsync(string email, string patientEmail, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(patientEmail) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Email, patient email, and password must be provided.");
            }

            try
            {
                await service.RegisterNewPatientIAMAsync(email, patientEmail, password);
                return Ok(new { message = "Patient signed in successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //GET /api/Patient/filtered?firstName=&lastName=&email=&phoneNumber=&medicalRecordNumber=&dateOfBirth=&gender=&sortBy=
        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<PatientDto>>> getPatientsFiltered([FromQuery] string? firstName, [FromQuery] string? lastName,
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            IEnumerable<string> roles = AuthService.GetGroupsFromToken(HttpContext);
            string adminEmail = AuthService.GetInternalEmailFromToken(HttpContext);

            if (!roles.Contains("admins"))
            {
                return Unauthorized();
            }
            var result = await service.DeletePatientAsync(id, adminEmail);
            if (result)
            {
                return Ok(new { message = "Patient deleted successfully." });
            }
            return NotFound(new { message = "Patient not found." });
        }

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

        [HttpPut("acceptPendingRequests")]
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

            var accepted = service.AcceptRequests(requests);
            if (accepted)
            {
                return Ok(new { message = "Patient requests accepted successfully." });
            }

            return NotFound(new { message = "Patient requests not found." });
        }

    }
}