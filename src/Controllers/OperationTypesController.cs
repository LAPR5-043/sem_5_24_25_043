using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sem_5_24_25_043.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using AppContext = src.Models.AppContext;
using src.Controllers.Services;

namespace sem_5_24_25_043.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationTypesController : ControllerBase
    {
        private readonly AppContext _context;

        private ManageOperationTypeService manageOperationTypeService;


        public OperationTypesController(AppContext context)
        {
             manageOperationTypeService = new ManageOperationTypeService(_context);
            _context = context;
        }

        // GET: api/OperationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationType>>> GetOperationTypes()
        {
            var operationTypes = await manageOperationTypeService.getAllOperationTypesAsync();
            return Ok(operationTypes);
        }

        // GET: api/OperationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationType>> GetOperationType(long id)
        {
            var operationType = await manageOperationTypeService.getOperationTypeAsync(id);
            if (operationType == null)
            {
                return NotFound();
            }
            return Ok(operationType);
        }

        // PUT: api/OperationTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperationType(long id, OperationTypeDto operationTypeDto)
        {
            if (id != operationTypeDto.OperationTypeID)
            {
                return BadRequest();
            }

            var operationType = await manageOperationTypeService.getOperationTypeAsync(id);
            if (operationType.Value == null)
            {
                return NotFound();
            }
            
            bool ret;

            if (operationTypeDto.IsActive != operationType.Value.isOperationActive()){
               ret =  manageOperationTypeService.deactivateOperationTypeAsync(operationType.Value, operationTypeDto.IsActive).Result;
            }else{ 
               ret = manageOperationTypeService.updateOperationTypeAsync(operationType.Value,operationTypeDto).Result;
            }

            
            if (!ret){  
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/OperationTypes
        [HttpPost]
        public ActionResult<OperationType> PostOperationType(OperationTypeDto operationTypeDto)
        {
            OperationType operationType = manageOperationTypeService.AddOperationTypeAsync(operationTypeDto).Result;

            return CreatedAtAction("GetOperationType", new { id = operationType.operationTypeId() }, operationType);
        }

        // DELETE: api/OperationTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationType(long id)
        {
            var operationType = await manageOperationTypeService.getOperationTypeAsync(id);
            if (operationType == null)
            {
                return NotFound();
            }
            manageOperationTypeService.deleteOperationType(operationType);


            return NoContent();
        }


    }
}