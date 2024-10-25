using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Mvc;
using sem_5_24_25_043;
using src.Services.IServices;
using src.Services.Services;


namespace src.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Auth service
        /// </summary>
        private readonly IAuthService authService;
        /// <summary>
        /// Patient service
        /// </summary>
        private readonly IPatientService patientService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authService"></param>
        public UserController(IAuthService authService, IPatientService patientService)
        {
            this.authService = authService;
            this.patientService = patientService;
        }

        [HttpPost("signup-patient")]
        public async Task<ActionResult<string>> SignUpPatientAsync(string name, string phoneNumber, string email, string patientEmail, string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(patientEmail) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { message = "Invalid patient data." });
            }

            try
            {
                await patientService.SignUpNewPatientIamAsync(name, phoneNumber, email, patientEmail, password);
                return Ok(new { message = "Patient signed up successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPost("login-user")]
        public async Task<ActionResult<string>> LogInUserAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { message = "Invalid user data." });
            }

            try
            {
                var tokens = await authService.SignInAsync(email, password);
                return Ok(new { accessToken = tokens.AccessToken, idToken = tokens.IdToken });
            }
            /*catch (NotAuthorizedException)
            {
                return Unauthorized();
            }*/
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        // /api/user/confirm-email?email=encryptedEmail
        [HttpGet("confirm-email")]
        public async Task<ActionResult<string>> ConfirmEmailAsync([FromQuery] string email)
        {
            try
            {
                await authService.ConfirmPatientEmailAsync(email);
                return Ok(new { message = "Email confirmed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }
    }



}