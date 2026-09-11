using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using HackNewsApp.Api.Authentication;
using HackNewsApp.Application.DTOs;

using Microsoft.AspNetCore.Authentication;

namespace HackNewsApp.Api.Controllers.v1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public sealed class AuthController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(JwtTokenGenerator jwtTokenGenerator, ILogger<AuthController> logger)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] AuthRequest request)
        {
            _logger.LogInformation("Login Request received - Username: {username}, Password:{password}......",
                request.Username, request.Password);

            if (request.Username != "admin" || request.Password != "password")
            {
                _logger.LogWarning("Wrong Username or Password provided!");

                return Unauthorized();
            }

            //generate token
            var token = _jwtTokenGenerator.GenerateToken("1", "admin", ["Admin"]);

            _logger.LogInformation("Token has now been generated: {token}", token);

            //add token and expiry time to response
            return Ok(new AuthResponse(token, DateTime.UtcNow.AddHours(1)));

        }

    }
}
