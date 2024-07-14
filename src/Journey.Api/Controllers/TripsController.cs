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
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody]RequestRegisterTripJson request)
    {
        var useCase = new RegisterTripUseCase();
        var response = useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var useCase = new GetAllTripsUseCase();
        var response = useCase.Execute();
        return Ok(response);
    }

    [HttpGet]
    [Route("{tripId}")]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid tripId)
    {
        var useCase = new GetByIdUseCase();
        var response = useCase.Execute(tripId);
        return Ok(response);
    }


    [HttpDelete]
    [Route("{tripId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute] Guid tripId)
    {
        var useCase = new DeleteTripUseCase();
        useCase.Execute(tripId);
        return NoContent();
    }

    [HttpPost]
    [Route("{tripId}/activity")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult RegisterActivity([FromRoute] Guid tripId, [FromBody] RequestRegisterActivityJson body)
    {
        var useCase = new RegisterActivityUseCase();
        var response = useCase.Execute(tripId, body);
        return Created(string.Empty, response);
    }
    [HttpPut]
    [Route("{activityId}/done")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult DoneActivity([FromRoute] Guid activityId)
    {
        var useCase = new CompleteActivityUseCase();
        useCase.Execute(activityId);
        return NoContent();
    }

}
