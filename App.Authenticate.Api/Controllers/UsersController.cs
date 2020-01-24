using App.Authenticate.Api.Entities.Request;
using App.Authenticate.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Authenticate.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IRegisterService _registerService;
        private readonly IAuthenticateService _authenticateService;

        public UsersController(
            IRegisterService registerService,
            IAuthenticateService authenticateService)
        {
            _registerService = registerService;
            _authenticateService = authenticateService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public void Register([FromBody] UserRegister request)
        {
            _registerService.Register(request);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserAuthenticate request)
        {
            var user = _authenticateService.Authenticate(request.Email, request.Password);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}