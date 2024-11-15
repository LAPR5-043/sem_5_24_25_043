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
            var surgeryRooms = await service.GetSurgeryRoomsAsync();
            return Ok(surgeryRooms);
        }

        // GET: api/Specialization/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SurgeryRoomDto>>> GetFilteredOperationTypes(string id)
        {
            var surgeryRoom = await service.GetSurgeryRoomAsync(id);
            return Ok(surgeryRoom);
        }
        

        

    }
}