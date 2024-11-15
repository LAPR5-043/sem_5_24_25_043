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
using Schedule;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService service;

        public AppointmentController(IAppointmentService service)
        {
            this.service = service;
        }

        // GET: api/Specialization
        [HttpGet]
        public async Task<ActionResult<List<ScheduleDto>>> GetAll([FromQuery] string roomId, [FromQuery] int day)
        {
           
            return Ok(service.GenerateApointmentsByRoomAndDate(roomId, day));
        }


        

        

    }
}