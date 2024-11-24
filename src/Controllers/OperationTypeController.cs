using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using src.Models;
using AppContext = src.Models.AppContext;
using src.Controllers.Services;
using src.Services.IServices;
using Domain.OperationTypeAggregate;
using Microsoft.AspNetCore.Authorization;
using sem_5_24_25_043;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "admins")] 
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
        public async Task<ActionResult<List<OperationTypeDto>>> GetOperationTypes()
        {
            var operationTypes = await service.getAllOperationTypesAsync();
            return Ok(operationTypes.Value);
        }

        // GET: api/OperationType/Filters?name=&specialization=&status=
        [HttpGet("Filtered")]
        public async Task<ActionResult<List<OperationTypeDto>>> GetFilteredOperationTypes([FromQuery] string name = null, [FromQuery] string specialization = null, [FromQuery] string status = null)
        {
            var operationTypes = await service.getFilteredOperationTypesAsync(name, specialization, status);
            return Ok(operationTypes.Value);
        }
        
        // PUT: api/OperationType/ChangeStatus/Knee Surgery
        [HttpPatch("/ChangeStatus/{id}")]
        public async Task<IActionResult> DeactivateOperationType(string id)
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
        public async Task<IActionResult> CreateOperationType([FromBody] OperationTypeDto operationType)
        {
            
            if (operationType == null)
            {
                return BadRequest(new { message = "Invalid operation type data." });
            }

            var adminEmail = User.Claims.First(claim => claim.Type == "custom:internalEmail").Value;
            try
            {
                var result = await service.createOperationTypeAsync(operationType, adminEmail);

                if (result)
                {
                    return Ok(new { message = "Operation type created successfully." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }

            return StatusCode(500, new { message = "An error occurred while creating the operation type." });
        }
        // PATCH: api/OperationType/edit/Knee Surgery
        [HttpPatch("edit/{id}")]
        public async Task<IActionResult> EditOperationType(string id, [FromBody] OperationTypeDto operationType)
        {
            
            if (operationType == null)
            {
                return BadRequest(new { message = "Invalid operation type data." });
            }

            var result = await service.editOperationTypeAsync(id, operationType);
            if (result)
            {
                return Ok(new { message = "Operation type edited successfully." });
            }
            return StatusCode(500, new { message = "An error occurred while editing the operation type." });
        }
        

    }
}