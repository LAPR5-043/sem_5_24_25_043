using System;
using Domain.OperationRequestAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        
        /// <summary>
        /// Delete operation request by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="confirmDelete"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationRequest(int id, [FromHeader(Name = "X-Confirm-Delete")] bool confirmDelete)
        {
            
            // Check confirmation before deletion
            if (!confirmDelete)
            {
                return BadRequest(new { message = "Deletion not confirmed." });
            }

            var result = await service.DeleteOperationRequestAsync(id);
            
            if (result)
            {
                return Ok(new { message = "Operation request deleted successfully." });
            }

            return NotFound(new { message = "Operation request not found." });
        }

        //PUT 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOperationRequest(int id, [FromBody] OperationRequestDto operationRequestDto)
        {
            if (operationRequestDto == null)
            {
                return BadRequest(new { message = "Invalid operation request data." });
            }

            var result = await service.UpdateOperationRequestAsync(id, operationRequestDto);

            if (result)
            {
                return Ok(new { message = "Operation request updated successfully." });
            }
            return NotFound(new { message = "Operation request not found." });
            
        }
    }
}