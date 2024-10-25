using System;
using Domain.OperationRequestAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sem_5_24_25_043;
using src.Services.IServices;

namespace src.Controllers
{
    [Authorize(Roles = "admins")]
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
            //var doctorEmail = User.Claims.First(claim => claim.Type == "custom:internalEmail").Value;

            if (operationRequestDto == null)
            {
                return BadRequest(new { message = "Invalid operation request data." });
            }

            try
            {
                var createdOperationRequest = await service.CreateOperationRequestAsync(operationRequestDto);
                if (createdOperationRequest)
                {
                    return Ok(new { message = "Operation Request created successfully." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
            return StatusCode(500, new { message = "An error occurred while creating the operation request." });

        }


        //GET /api/OperationRequest/filtered?firstName=&lastName=&email=&phoneNumber=&medicalRecordNumber=&dateOfBirth=&gender=&sortBy=
        [Authorize(Roles = "medic")]
        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<OperationRequestDto>>> GetOperationRequestFiltered([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] string? operationType,
            [FromQuery] string? priority, [FromQuery] string? status, [FromQuery] string? sortBy)
        {

            //var doctorEmail = User.Claims.First(claim => claim.Type == "custom:internalEmail").Value;

            var OperaionRequest = await service.GetOperationRequestFilteredAsync(firstName, lastName, operationType, priority, status, sortBy);
            if (OperaionRequest == null)
            {
                return NotFound();
            }

            return Ok(OperaionRequest);
        }

        // GET: api/OperationRequest/5
        [Authorize(Roles = "medic")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOperationRequestById(string id)
        {
            //var doctorEmail = User.Claims.First(claim => claim.Type == "custom:internalEmail").Value;

            var operationRequest = await service.GetOperationRequestByIdAsync(id);
            if (operationRequest == null)
            {
                return NotFound(new { message = "Operation Request not found." });

            }

            return Ok(operationRequest);
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
        [Authorize(Roles = "medic")]
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