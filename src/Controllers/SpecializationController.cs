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

    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService service;

        public SpecializationController(ISpecializationService service)
        {
            this.service = service;
        }

        // GET: api/Specialization
        [HttpGet]
        public async Task<ActionResult<List<SpecializationDto>>> GetAllSpecializations()
        {
            
            try
            {
                var specializations = await service.GetSpecializationsAsync();
                return Ok(specializations);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Specialization/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SpecializationDto>>> GetFilteredOperationTypes(string id)
        {
            if (id == null)
            {
                return BadRequest(new { message = "Invalid Specialization ID." });
            }

            try
            {
                var specialization = await service.GetSpecializationAsync(id);
                if (specialization == null)
                {
                    return NotFound(new { message = "Specialization not found." });
                }
                return Ok(specialization);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<SpecializationDto>> CreateSpecialization([FromBody] SpecializationDto specializationDto)
        {
            if (specializationDto == null)
            {
                return BadRequest(new { message = "Invalid Specialization data." });
            }

            try
            {
                Console.WriteLine(specializationDto.SpecializationName);

                var createdSpecialization =  await service.CreateSpecializationAsync(specializationDto);
                if (createdSpecialization != null)
                {
                    return Ok(new { message = "Specialization created successfully." });

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
            return StatusCode(500, new { message = "An error occurred while creating the Specialization." });
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<SpecializationDto>> UpdateSpecialization(string id, [FromBody] SpecializationDto specializationDto)
        {
            if (id == null)
            {
                return BadRequest(new { message = "Invalid Specialization ID." });
            }

            if (specializationDto == null)
            {
                return BadRequest(new { message = "Invalid Specialization data." });
            }

            try
            {
                var updatedSpecialization = await service.UpdateSpecializationAsync(id, specializationDto);
                if (updatedSpecialization != null) 
                {
                    return Ok(new { message = "Specialization updated successfully." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
            return StatusCode(500, new { message = "An error occurred while updating the Specialization." });
        }

        

    }
}