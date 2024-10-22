using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using sem_5_24_25_043;
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


        // POST: api/Staff/Create
        [HttpPost("Create")]
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


        //GET /api/Staff/filtered?firstName=&lastName=&license=&email=&specialization=&sortBy=
        [HttpGet("filtered")]
        
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetStaffsFiltered([FromQuery] string? firstName, [FromQuery] string? lastName, 
                                                                                [FromQuery] string? email, [FromQuery] string? specialization , [FromQuery] string? sortBy)
        {
            IEnumerable<string> roles = AuthService.GetGroupsFromToken(HttpContext);

            if (!roles.Contains("admins"))
            {
                return Unauthorized();
            }
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
    // [HttpPatch]
}