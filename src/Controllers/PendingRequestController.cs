using Domain.PatientAggregate;
using Microsoft.AspNetCore.Mvc;
using src.Services.IServices;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PendingRequestController : ControllerBase
    {
        private readonly IPendingRequestService service;

        public PendingRequestController(IPendingRequestService service)
        {
            this.service = service;
        }





    }
}