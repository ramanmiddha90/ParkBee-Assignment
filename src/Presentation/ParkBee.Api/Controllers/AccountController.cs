using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkBee.Core.Application.Common.Models;
using ParkBee.Core.Application.Token;
using System;
using System.Threading.Tasks;

namespace ParkBee.Assessment.API.Controllers
{
    public class AccountController : Controller
    {
        private ILogger _logger;
        private IMediator _mediator;

        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("token"), AllowAnonymous]
        [ProducesDefaultResponseType(typeof(TokenRequestQuery))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TokenResult>> RequestToken([FromBody]TokenRequestQuery request)
        {
            try
            {
                var response = await _mediator.Send(request);

                if (response == null)
                    return Unauthorized();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RefreshDoorStatus failure-{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Token service not available. Please try again later!");
            }
        }
    }
}