using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using AppContext = src.Models.AppContext;
using src.Controllers.Services;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
  

        private ManageStaffService manageStaffService;

        private readonly AppContext _appContext;

        public StaffController(AppContext appContext)
        {
            _appContext = appContext;
            manageStaffService = new ManageStaffService(_appContext);
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationType>>> GetStaffs()
        {
            var operationTypes = await manageStaffService.getAllStaffAsync();
            return Ok(operationTypes);
        }

        //GET /api/Staff/filtered?firstName=&lastName=&license=&email=&specialization=&sortBy=
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaffsFiltered([FromQuery] string? firstName, [FromQuery] string? lastName, 
                                                                                [FromQuery] string? email, [FromQuery] string? specialization , [FromQuery] string? sortBy)
        {
            var staff = await manageStaffService.getStaffsFilteredAsync(firstName, lastName, email, specialization, sortBy);
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
            var staff = await manageStaffService.getStaffAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        


    }
}