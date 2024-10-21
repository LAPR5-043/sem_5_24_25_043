using Domain.PatientAggregate;
using Microsoft.AspNetCore.Mvc;
using src.Services.IServices;
using src.Services.Services;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PatientController : ControllerBase
    {
        private readonly IPatientService service;

        public PatientController(IPatientService service)
        {
            this.service = service;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            var result = await service.DeletePatientAsync(id);
            if (result)
            {
                return Ok(new { message = "Patient deleted successfully." });
            }
            return NotFound(new { message = "Patient not found." });
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await service.GetPatientByIdAsync(id);
            if (patient != null)
            {
            return Ok(patient);
            }

            return NotFound(new { message = "Patient not found." });
        }

        [HttpPut("personalData/{id}")]

        public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientDto patientDto)
        {
            if (patientDto == null)
            {
            return BadRequest(new { message = "Invalid patient data." });
            }

            var patientExists = await service.GetPatientByIdAsync(id);
            if (patientExists==null)
            {
                return NotFound(new { message = "Patient not found." });
            }

            var result = await service.UpdatePatientAsync(id, patientDto);

            if (result){
                return Ok(new { message = "Patient updated successfully." });
            }


            return NotFound(new { message = "Patient not found." });
        }

        [HttpPut]
        public async Task<IActionResult> AcceptPatientPendingRequests( [FromBody] List<long> requestIds)
        {
            if (requestIds == null)
            {
            return BadRequest(new { message = "Invalid pending requests data." });
            }

            var accepted = service.AcceptRequests(requestIds);
            if (accepted)
            {
            return Ok(new { message = "Patient requests accepted successfully." });
            }

            return NotFound(new { message = "Patient requests not found." });
        }

    }
}