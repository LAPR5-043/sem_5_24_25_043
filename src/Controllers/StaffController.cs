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

    public class StaffController : ControllerBase
    {
        private readonly IStaffService service;

        public StaffController(IStaffService service)
        {
            this.service = service;
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetStaffs()
        {
            var staffs = await service.getAllStaffAsync();
            List<StaffDto> dtos = new List<StaffDto>();

            return Ok(staffs);
        }

        //GET /api/Staff/filtered?firstName=&lastName=&license=&email=&specialization=&sortBy=
        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetStaffsFiltered([FromQuery] string? firstName, [FromQuery] string? lastName, 
                                                                                [FromQuery] string? email, [FromQuery] string? specialization , [FromQuery] string? sortBy)
        {
            var staff = await service.getStaffsFilteredAsync(firstName, lastName, email, specialization, sortBy);
            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }

        // GET: api/Staff/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetStaff(string id)
        {
            var staff = await service.getStaffAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        // POST: api/Staff
        [HttpPost]
        public async Task<ActionResult<StaffDto>> CreateStaff([FromBody] StaffDto staffDto)
        {
            if (staffDto == null)
            {
                return BadRequest("Staff data is null.");
            }

            var createdStaff = await service.CreateStaffAsync(staffDto);
            if (createdStaff == null)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return CreatedAtAction(nameof(GetStaff), new { id = createdStaff.StaffID }, createdStaff);
        }

        // PUT: api/Staff/isActive/5
        [HttpPut("/isActive/{id}")]
        public async Task<IActionResult> UpdateIsActive(string id)
        {
            var result = await service.UpdateIsActiveAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(new { message = "Patient deativated with success." });
        }
        


    }
}