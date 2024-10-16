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
            return await _context.OperationTypes.ToListAsync();
        }

        // GET: api/OperationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationType>> GetOperationType(long id)
        {
            var operationType = await _context.OperationTypes.FindAsync(id);

            if (operationType == null)
            {
                return NotFound();
            }

            return operationType;
        }

        // PUT: api/OperationTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperationType(long id, OperationTypeDto operationTypeDto)
        {
            if (id != operationTypeDto.OperationTypeID)
            {
                return BadRequest();
            }

            var operationType = await _context.OperationTypes.FindAsync(id);
            if (operationType == null)
            {
                return NotFound();
            }


            operationType.changeOperationStatus(operationTypeDto.IsActive); 
            operationType.changeOperationType(operationTypeDto.OperationTypeName);
            operationType.changeOperationDuration(operationTypeDto.Hours, operationTypeDto.Minutes);
            _context.Entry(operationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperationTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OperationTypes
        [HttpPost]
        public async Task<ActionResult<OperationType>> PostOperationType(OperationTypeDto operationTypeDto)
        {
            var operationType = new OperationType(
                operationTypeDto.OperationTypeID,
                operationTypeDto.IsActive,
                operationTypeDto.OperationTypeName,
                operationTypeDto.Hours,
                operationTypeDto.Minutes
            );

            _context.OperationTypes.Add(operationType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperationType", new { id = operationType.operationTypeId() }, operationType);
        }

        // DELETE: api/OperationTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationType(long id)
        {
            var operationType = await _context.OperationTypes.FindAsync(id);
            if (operationType == null)
            {
                return NotFound();
            }

            _context.OperationTypes.Remove(operationType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperationTypeExists(long id)
        {
            return _context.OperationTypes.Any(e => e.operationTypeId() == id);
        }
    }
}