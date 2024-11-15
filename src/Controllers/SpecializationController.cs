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
            var specializations = await service.GetSpecializationsAsync();
            return Ok(specializations);
        }

        // GET: api/Specialization/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SpecializationDto>>> GetFilteredOperationTypes(string id)
        {
            var specialization = await service.GetSpecializationAsync(id);
            return Ok(specialization);
        }
        

        

    }
}