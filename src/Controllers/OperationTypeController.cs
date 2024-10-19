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

            var result = await service.CreateOperationTypeAsync(operationType);
            if (result)
            {
                return Ok(new { message = "Operation type created successfully." });
            }
            return StatusCode(500, new { message = "An error occurred while creating the operation type." });
        }

    }
}