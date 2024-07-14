
using Journey.Application.UseCases.Validators;
using Journey.Communication.Enums;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases;
public class RegisterActivityUseCase
{
    public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
    {
        var dbContext = new JourneyDbContext();
        var trip = dbContext.Trips.FirstOrDefault(trip => trip.Id.Equals(tripId));
        if (trip is null)
        {
            throw new NotFoundException("Trip Not Found");
        }
        Validate(trip, request);
        var entity = new Activity
        {
            Name = request.Name,
            TripId = tripId,
            Date = request.Date,
            Status = Infrastructure.Enums.ActivityStatus.Pending
        };
        dbContext.Activities.Add(entity);
        dbContext.SaveChanges();
        return new ResponseActivityJson
        {
            Id = entity.Id,
            Status = (ActivityStatus)entity.Status,
            Date = entity.Date,
            Name = entity.Name,
        };
    }

    private void Validate(Trip trip, RequestRegisterActivityJson request)
    {

        var validator = new RegisterActivityValidator();
        var result = validator.Validate(request);
        if((trip.StartDate <= request.Date && trip.EndDate >= request.Date) is false)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("Date Activity Wrong", "Date Incorrect"));
        } 
        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
