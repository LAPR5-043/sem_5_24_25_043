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
    [ApiController]

    public class SurgeryRoomController : ControllerBase
    {
        private readonly ISurgeryRoomService service;

        public SurgeryRoomController(ISurgeryRoomService service)
        {
            this.service = service;
        }

        // GET: api/Specialization
        [HttpGet]
        public async Task<ActionResult<List<SurgeryRoomDto>>> GetAllSpecializations()
        {
            try
            {
                var surgeryRooms = await service.GetSurgeryRoomsAsync();
                return Ok(surgeryRooms);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        
        }

        // GET: api/Specialization/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SurgeryRoomDto>>> GetFilteredOperationTypes(string id)
        {
            try
            {
                var surgeryRoom = await service.GetSurgeryRoomAsync(id);
                return Ok(surgeryRoom);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<SurgeryRoomDto>> CreateSurgeryRoom(SurgeryRoomDto surgeryRoomDto)
        {
            try
            {
                return await service.CreateSurgeryRoomAsync(surgeryRoomDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }        

        

    }
}