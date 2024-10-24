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