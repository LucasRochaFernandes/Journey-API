using Journey.Application.UseCases;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody]RequestRegisterTripJson request)
    {
        try
        {
            var useCase = new RegisterTripUseCase();
            var response = useCase.Execute(request);
            return Created(string.Empty, response);
        } catch (JourneyException e) { 
            return BadRequest(e.Message);
        } catch 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Unknown Error");
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var useCase = new GetAllTripsUseCase();
        var response = useCase.Execute();
        return Ok(response);
    }

    [HttpGet]
    [Route("{tripId}")]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid tripId)
    {
        try
        {
            var useCase = new GetByIdUseCase();
            var response = useCase.Execute(tripId);
            return Ok(response);
        } catch (JourneyException e)
        {
            return NotFound(e.Message);
        } catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Unknown Error");
        }
    }
}
