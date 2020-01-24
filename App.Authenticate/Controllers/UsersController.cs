using App.Authenticate.Models.Request;
using App.Authenticate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Authenticate.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IAuthenticateService _authenticateService;

        public UsersController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User request)
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