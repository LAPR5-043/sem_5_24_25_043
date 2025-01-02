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
using src.Domain.AppointmentAggregate;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService service;

        public static string nothing_to_schedule = "Nothing To Schedule";
        public static string schedule_with_success = "Schedule With Success";
        public static string room_full = "The Room is Full";
        public AppointmentController(IAppointmentService service)
        {
            this.service = service;
        }


        [HttpGet("day")]
        public ActionResult<List<AppointmentDto>> GetDayAppointments([FromQuery] int day)
        {
            try
            {
                if (day == 0)
                {
                    return BadRequest();
                }
                var appoints = service.GetDayAppointmentsAsync(day);
                if (appoints == null)
                {
                    return NotFound();
                }
                return Ok(appoints);
            }
            catch (Exception e)
            {
                e.StackTrace.ToString();
                return BadRequest();
            }
        }

        [HttpGet("All")]
        public ActionResult<List<AppointmentDto>> GetAllAppointments()
        {
            try
            {
                var appoints = service.GetAllAppointmentsAsync();
                if (appoints == null)
                {
                    return NotFound();
                }
                return Ok(appoints);
            }
            catch (Exception e)
            {
                e.StackTrace.ToString();
                return BadRequest();
            }
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
                if (e.Message == room_full)
                {
                    return Ok(new { message = room_full });
                }
                
                return StatusCode(500, e.Message);
               
            }

        
        }

        [HttpGet("GenerateToMultipleRooms")]
        public async Task<ActionResult<GeneticResponseDto>> GetAppointmentsForTheDay([FromQuery] int day)
        {   
            try
            {
                if ( day == 0)
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
                GeneticResponseDto response =  await service.GenerateApointmentsByDateAsync( day);

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
                if (e.Message == room_full)
                {
                    return Ok(new { message = room_full });
                }
                
                return StatusCode(500, e.Message);
               
            }

        
        }

    [HttpGet("request")]
    public async Task<ActionResult<AppointmentDto>> GetAppointmentByRequestID([FromQuery] string requestID)
    {
        try
        {
            if (string.IsNullOrEmpty(requestID))
            {
                return BadRequest(new { message = "Request ID cannot be null or empty." });
            }

            var appoint = await service.GetAppointmentByRequestIDAsync(requestID);
            if (appoint == null)
            {
                return NotFound(new { message = "Appointment not found." });
            }

            return Ok(appoint);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "Appointment not found."});
        }
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentDto>> CreateAppointment([FromBody] AppointmentDto appointmentDto)
    {
        try
        {
            if (appointmentDto == null)
            {
                return BadRequest(new { message = "Appointment cannot be null." });
            }

            var appoint = await service.createAppointmentAsync(appointmentDto);
            if (appoint == null)
            {
                return NotFound(new { message = "Appointment not found." });
            }

            return Ok(appoint);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = e.Message });
        }


    }

    [HttpPatch]
    public async Task<ActionResult<AppointmentDto>> UpdateAppointment([FromBody] AppointmentDto appointmentDto)
    {
        try
        {
            Console.WriteLine("appointmentDto");
            if (appointmentDto == null)
            {
                return BadRequest(new { message = "Appointment cannot be null." });
            }
            Console.WriteLine("appointmentDto1");
            var appoint = await service.updateAppointmentAsync(appointmentDto);
            Console.WriteLine("appointmentDto2");
            if (appoint == null)
            {
                return NotFound(new { message = "Appointment not found." });
            }

            return Ok(appoint);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = e.Message });
        }
    }
    }
}