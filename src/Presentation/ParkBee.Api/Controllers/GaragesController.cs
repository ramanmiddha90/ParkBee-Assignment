using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkBee.Core.Application.Common.Exceptions;
using ParkBee.Core.Application.Garages.Commands;
using ParkBee.Core.Application.Garages.Queries;
using ParkBee.Core.Domain.Exceptions;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkBee.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize()]
    public class GaragesController : BaseController
    {
        private ILogger _logger;

        private IMediator _mediator;
        public GaragesController(IMediator mediator, ILogger<GaragesController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Get garage by garage id
        /// </summary>
        /// <param name="garageId"></param>
        /// <returns></returns>
        [HttpGet("{garageId}")]
        [ProducesDefaultResponseType(typeof(GarageDetail))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<GarageDetail>> GetGarageAsync(Guid garageId)
        {
            if (garageId == Guid.Empty) return new BadRequestResult();
            try
            {
                var response = await _mediator.Send(new GetGarageQueryByGarageId(garageId));

                if (response == null)
                    return StatusCode(StatusCodes.Status404NotFound);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RefreshDoorStatus failure-{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Couldn't get garage with garageid {garageId}");
            }
        }

        /// <summary>
        /// Get garage doors for a garage
        /// </summary>
        /// <param name="garageId"></param>
        /// <returns></returns>
        [HttpGet("{garageId}/doors")]
        [ProducesDefaultResponseType(typeof(List<Door>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Door>>> GetGarageDoors(Guid garageId)
        {
            if (garageId == Guid.Empty) return new BadRequestResult();
            try
            {
                var response = await _mediator.Send(new GetGarageDoorsQuery(garageId));

                if (response == null)
                    return StatusCode(StatusCodes.Status404NotFound);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RefreshDoorStatus failure-{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Couldn't get garage with garageid {garageId}");
            }
        }

        /// <summary>
        /// Refresh garage door status
        /// </summary>
        /// <param name="request">Request to update garage door status based on garage id and door id</param>
        /// <returns></returns>
        [HttpPost("RefreshDoorStatus")]
        [ProducesDefaultResponseType(typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshDoorStatusAsync([FromBody] RefreshGarageDoorStatusCommand request)
        {
            if (request == null) return new BadRequestResult();

            try
            {
                var garage = await _mediator.Send(new GetGarageQueryByGarageId(request.GargeId));

                if (garage ==null)
                    return new NotFoundObjectResult($"Couldn't find garage with garage id {request.GargeId}");

                await _mediator.Send(new RefreshGarageDoorStatusCommand(request.GargeId,request.DoorId,request.IPAddress,request.Status));
                return NoContent();
            }
            catch(ValidationException ex)
            {
                _logger.LogError($"RefreshDoorStatus failure-{ex.Message}");
                throw;
            }
            catch (DoorNotFoundException ex)
            {
                _logger.LogError($"RefreshDoorStatus failure-{ex.Message}");
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (DoorNotReachableException ex)
            {
                _logger.LogError($"RefreshDoorStatus failure-{ex.Message}");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RefreshDoorStatus failure-{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Couldn't open door with id {request.DoorId}");
            }
        }

    }
}


