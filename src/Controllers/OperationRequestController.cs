using System;
using Domain.OperationRequestAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sem_5_24_25_043;
using src.Services.IServices;

namespace src.Controllers
{
    [Authorize(Roles = "admins, medic")]
    [Route("api/[controller]")]
    [ApiController]
    public class OperationRequestController : ControllerBase
    {
        private readonly IOperationRequestService service;

        public OperationRequestController(IOperationRequestService service)
        {
            this.service = service;
        }


        /// <summary>
        /// Create an Operation Request
        /// </summary>
        /// <param name="operationRequestDto"></param>
        /// <returns></returns>
        // POST: api/Patient/Create
        [Authorize(Roles = "medic")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateOperationRequest([FromBody] OperationRequestDto operationRequestDto)
        {
            if (operationRequestDto == null)
            {
                return BadRequest(new { message = "Operation request data is invalid." });
            }

            try
            {
                var email = User.Claims.FirstOrDefault(c => c.Type == "custom:internalEmail")?.Value;
                bool isCreated = await service.CreateOperationRequestAsync(operationRequestDto, email);

                if (isCreated)
                {
                    return CreatedAtAction(nameof(CreateOperationRequest), new { id = operationRequestDto.RequestId }, operationRequestDto);
                }
                return BadRequest(new { message = "Failed to create operation request." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the operation request.", error = ex.Message });
            }
        }


        //GET /api/OperationRequest/admin/filtered?firstName=&lastName=&email=&phoneNumber=&medicalRecordNumber=&dateOfBirth=&gender=&sortBy=
        [Authorize(Roles = "admins")]
        [HttpGet("admin/filtered")]
        public async Task<ActionResult<IEnumerable<OperationRequestDto>>> GetOperationRequestFiltered([FromQuery(Name = "Doctor ID")] string? doctorID, [FromQuery(Name = "Patient ID")] string? patientID, [FromQuery(Name = "Patient First Name")] string? patientFirstName, [FromQuery(Name = "Patient Last Name")] string? patientLastName, [FromQuery(Name = "Operation Type")] string? operationType, [FromQuery(Name = "Priority")] string? priority, [FromQuery(Name = "Sort by")] string? sortBy)
        {
            try
            {
                var operationRequest = await service.GetOperationRequestFilteredAsync(doctorID, patientID, patientFirstName, patientLastName, operationType, priority, sortBy);
                if (operationRequest != null)
                {
                    return Ok(operationRequest);
                }

                return NotFound(new { message = "Operation request not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching the operation request.", error = ex.Message });
            }
        }

        // GET: api/OperationRequest/doctor/submitted/?selfRequested=&patientID=&patientFirstName=&patientLastName=&operationType=&priority=&sortBy=
        [Authorize(Roles = "medic")]
        [HttpGet("doctor/submitted")]
        public async Task<ActionResult<IEnumerable<OperationRequestDto>>> GetDoctorOperationRequests([FromQuery(Name = "Patient ID")] string? patientID, [FromQuery(Name = "Patient First Name")] string? patientFirstName, [FromQuery(Name = "Patient Last Name")] string? patientLastName, [FromQuery(Name = "Operation Type")] string? operationType, [FromQuery(Name = "Priority")] string? priority, [FromQuery(Name = "Sort by")] string? sortBy)
        {
            var doctorEmail = User.Claims.First(claim => claim.Type == "custom:internalEmail").Value;

            try
            {
                var operationRequest = await service.GetDoctorOperationRequestsAsync(doctorEmail, patientID, patientFirstName, patientLastName, operationType, priority, sortBy);
                if (operationRequest != null)
                {
                    return Ok(operationRequest);
                }

                return NotFound(new { message = "Operation request not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching the operation request.", error = ex.Message });
            }
        }

        // GET: api/OperationRequest/doctor/{PatientID}
        [Authorize(Roles = "medic")]
        [HttpGet("doctor/{PatientID}")]
        public async Task<ActionResult<List<OperationRequestDto>>> GetOperationRequestByPatientId(string PatientID)
        {
            try
            {
                var operationRequest = await service.GetOperationRequestByPatientIdAsync(PatientID);
                if (operationRequest != null)
                {
                    return Ok(operationRequest);
                }

                return NotFound(new { message = "Operation requests by patient not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching the operation request.", error = ex.Message });
            }
        }


        /// <summary>
        /// Delete operation request by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="confirmDelete"></param>
        /// <returns></returns>
        [Authorize(Roles = "medic")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationRequest(int id, [FromHeader(Name = "X-Confirm-Delete")] bool confirmDelete)
        {

            var doctorEmail = User.Claims.First(claim => claim.Type == "custom:internalEmail").Value;

            // Check confirmation before deletion
            if (!confirmDelete)
            {
                return BadRequest(new { message = "Deletion not confirmed." });
            }

            try
            {
                var result = await service.DeleteOperationRequestAsync(id, doctorEmail);

                if (result)
                {
                    return Ok(new { message = "Operation request deleted successfully." });
                }

                return NotFound(new { message = "Operation request not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the operation request.", error = ex.Message });
            }
        }

        //PUT 
        [Authorize(Roles = "medic,admins")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateOperationRequest(int id, [FromBody] OperationRequestDto operationRequestDto)
        {

            var doctorEmail = User.Claims.First(claim => claim.Type == "custom:internalEmail").Value;
            if (operationRequestDto == null)
            {
                return BadRequest(new { message = "Invalid operation request data." });
            }

            bool result = await service.UpdateOperationRequestAsync(id, operationRequestDto, doctorEmail);

            if (result)
            {
                return Ok(new { message = "Operation request updated successfully." });
            }
            return NotFound(new { message = "Operation request not found." });
        }
    }
}
