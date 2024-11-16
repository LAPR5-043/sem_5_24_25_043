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

        public static string nothing_to_schedule = "Nothing To Schedule";
        public static string schedule_with_success = "Schedule With Success";
        public AppointmentController(IAppointmentService service)
        {
            this.service = service;
        }


        [HttpGet]
        public async Task<ActionResult<PlanningResponseDto>> GetAppointmentsForTheRoomAndDay([FromQuery] string roomId, [FromQuery] int day)
        {   
            try
            {
                if (roomId == null || day == 0)
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }

            try
            {
                PlanningResponseDto response =  await service.GenerateApointmentsByRoomAndDateAsync(roomId, day);

                if (response == null)
                {
                    return NotFound();
                }

                return Ok(new { message = schedule_with_success, schedule = response });

            }
            catch(Exception e)
            {
                if (e.Message == nothing_to_schedule)
                {
                    return Ok(new { message = nothing_to_schedule });
                }
                else
                {
                    return StatusCode(500);
                }
               
            }

        
        }


        

        

    }
}