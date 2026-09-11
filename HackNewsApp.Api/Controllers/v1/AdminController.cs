using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HackNewsApp.Api.Authorisation;
using HackNewsApp.Application.Contracts.Caching;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackNewsApp.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = AuthorisationPolicies.AdminOnly)]
    public sealed class AdminController : ControllerBase
    {
        private readonly ICacheService _cache;

        public AdminController(ICacheService cache)
        {
            _cache = cache;
        }

        [HttpDelete("cache")]
        public IActionResult ClearHackNewsCache()
        {
            _cache.Remove("hacknews-items");

            return NoContent();
        }

    }
}
