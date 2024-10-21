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


        // GET: 1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationRequest>>> GetOperationRequest()
        {
            var operationTypes = await service.getAllOperationRequestsAsync();
            return Ok(operationTypes);
        }

        /*
        //GET /api/OperationRequest/filtered?firstName=&lastName=&license=&email=&specialization=&sortBy=
        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<OperationRequest>>> GetOperationRequestsFiltered([FromQuery] string? firstName, [FromQuery] string? lastName,
                                                                                [FromQuery] string? operationType, [FromQuery] string? priority, string? sortBy)
        {
            var operationRequest = await service.getOperationRequestsFilteredAsync(firstName, lastName, operationType, priority, sortBy);
            if (operationRequest == null)
            {
                return NotFound();
            }

            return Ok(operationRequest);
        }
        */

        // GET: api/OperationRequest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationRequest>> GetOperationRequest(string id)
        {
            var operationRequest = await service.getOperationRequestAsync(id);
            if (operationRequest == null)
            {
                return NotFound();
            }
            return Ok(operationRequest);
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