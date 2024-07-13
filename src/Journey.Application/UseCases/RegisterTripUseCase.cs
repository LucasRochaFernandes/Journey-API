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
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new JourneyException("Name is Null or Empty");
        }
        if (request.StartDate < DateTime.UtcNow)
        {
            throw new JourneyException("Start Date is with wrong value");
        }

        if (request.StartDate >= request.EndDate)
        {
            throw new JourneyException("Start Date is smaller than End Date");
        }
    }
}
