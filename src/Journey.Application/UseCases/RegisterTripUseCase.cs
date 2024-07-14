using Journey.Application.UseCases.Validators;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases;
public class RegisterTripUseCase
{
    public ResponseShortTripJson Execute(RequestRegisterTripJson request)
    {
        Validate(request);
        var dbContext = new JourneyDbContext();
        var entity = new Trip {
        Name = request.Name,
        EndDate = request.EndDate,
        StartDate = request.StartDate
        };
        dbContext.Trips.Add(entity);
        dbContext.SaveChanges();
        return new ResponseShortTripJson
        {
            EndDate = entity.EndDate,
            StartDate = entity.StartDate,
            Name = entity.Name,
            Id = entity.Id
        };
    }

    private void Validate(RequestRegisterTripJson request)
    {
        var validator = new RegisterTripValidator();
        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
