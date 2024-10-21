using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Mvc;
using sem_5_24_25_043;
using src.Services.IServices;


namespace src.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AuthService authService;

        public  UserController(AuthService authService)
        {
            this.authService = authService;
        }

        
    }



}