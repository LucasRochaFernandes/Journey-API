using Journey.Communication.Responses;
using Journey.Infrastructure;

namespace Journey.Application.UseCases;
public class GetAllTripsUseCase
{
    public ResponseTripsJson Execute()
    {
        var dbContext = new JourneyDbContext();
        var trips = dbContext.Trips.ToList();
        return new ResponseTripsJson
        {
        Trips = trips.Select(trip => new ResponseShortTripJson { 
            Id = trip.Id, 
            Name = trip.Name, 
            EndDate = trip.EndDate,
            StartDate = trip.StartDate
        }).ToList(),
        };
    }
}
