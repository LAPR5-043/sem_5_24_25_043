using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using AppContext = src.Models.AppContext;
using src.Controllers.Services;
using src.Services.IServices;

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

    }
}