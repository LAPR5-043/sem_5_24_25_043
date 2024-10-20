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
    }
}