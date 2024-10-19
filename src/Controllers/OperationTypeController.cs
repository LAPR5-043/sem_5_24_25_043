using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using AppContext = src.Models.AppContext;
using src.Controllers.Services;
using src.Services.IServices;
using Domain.OperationTypeAggregate;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OperationTypeController : ControllerBase
    {
        private readonly IOperationTypeService service;

        public OperationTypeController(IOperationTypeService service)
        {
            this.service = service;
        }

        // GET: api/OperationType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationTypeDto>>> getOperationTypes()
        {
            var operationTypes = await service.getAllOperationTypesAsync();
            return Ok(operationTypes);
        }

        // GET: api/OperationType/Filters?name=&specialization=&status=
        [HttpGet("Filtered")]
        public async Task<ActionResult<IEnumerable<OperationTypeDto>>> getFilteredOperationTypes([FromQuery] string name = null, [FromQuery] string specialization = null, [FromQuery] string status = null)
        {
            var operationTypes = await service.getFilteredOperationTypesAsync(name, specialization, status);
            return Ok(operationTypes);
        }
        // PUT: api/OperationType/ChangeStatus/Knee Surgery
        [HttpPut("/ChangeStatus/{id}")]
        public async Task<IActionResult> deactivateOperationType(string id)
        {
            var result = await service.deactivateOperationTypeAsync(id);
            if (result)
            {
                return Ok(new { message = "Operation type deactivated successfully." });
            }
            return NotFound(new { message = "Operation type not found." });
        }

        // POST: api/OperationType/Create
        [HttpPost("Create")]
        public async Task<IActionResult> createOperationType([FromBody] OperationTypeDto operationType)
        {
            if (operationType == null)
            {
                return BadRequest(new { message = "Invalid operation type data." });
            }

            var result = await service.createOperationTypeAsync(operationType);
            if (result)
            {
                return Ok(new { message = "Operation type created successfully." });
            }
            return StatusCode(500, new { message = "An error occurred while creating the operation type." });
        }

    }
}