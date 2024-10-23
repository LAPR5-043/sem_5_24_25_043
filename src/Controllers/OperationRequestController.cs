using System;
using Domain.OperationRequestAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sem_5_24_25_043;
using src.Services.IServices;

namespace src.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OperationRequestController : ControllerBase
    {
        private readonly IOperationRequestService service;

        public OperationRequestController(IOperationRequestService service)
        {
            this.service = service;
        }


        //GET /api/OperationRequest/filtered?firstName=&lastName=&email=&phoneNumber=&medicalRecordNumber=&dateOfBirth=&gender=&sortBy=
        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<OperationRequestDto>>> getOperationRequestFiltered([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] string? operationType,
                                                                        [FromQuery] string? priority, [FromQuery] string? status, [FromQuery] string? sortBy)
        {

            var OperaionRequest = await service.GetOperationRequestFilteredAsync(firstName, lastName, operationType, priority, status, sortBy);
            if (OperaionRequest == null)
            {
                return NotFound();
            }

            return Ok(OperaionRequest);
        }

        // GET: api/OperationRequest/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOperationRequestById(string id)
        {
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationRequest(int id, [FromHeader(Name = "X-Confirm-Delete")] bool confirmDelete)
        {
            IEnumerable<string> roles = AuthService.GetGroupsFromToken(HttpContext);

            if (!roles.Contains("medic"))
            {
                return Unauthorized();
            }

            var doctorEmail = AuthService.GetInternalEmailFromToken(HttpContext);

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
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateOperationRequest(int id, [FromBody] OperationRequestDto operationRequestDto)
        {

            IEnumerable<string> roles = AuthService.GetGroupsFromToken(HttpContext);

            string doctorEmail = AuthService.GetInternalEmailFromToken(HttpContext);

            if (!roles.Contains("medic"))
            {
                return Unauthorized();
            }
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