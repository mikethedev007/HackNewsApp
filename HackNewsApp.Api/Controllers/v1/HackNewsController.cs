using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HackNewsApp.Api.Authorisation;
using HackNewsApp.Application.Contracts.ResultExecution;
using HackNewsApp.Application.Contracts.Services;
using HackNewsApp.Application.DTOs;
using Microsoft.AspNetCore.RateLimiting;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackNewsApp.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Authorize(Policy = AuthorisationPolicies.CanViewHackNews)]
    [EnableRateLimiting("ApiLimiter")]
    public sealed class HackNewsController : ControllerBase
    {
        private readonly IHackNewsService _service;
        private readonly IResultExecutor _resultExecutor;

        public HackNewsController(IHackNewsService service, IResultExecutor resultExecutor)
        {
            _service = service;
            _resultExecutor = resultExecutor;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = AuthorisationPolicies.CanViewHackNews)]
        public async Task<IActionResult> Get([FromQuery] HackNewsQueryRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _service.GetNewsAsync(request, cancellationToken);

            return _resultExecutor.Execute(this, result);
        }

    }
}
