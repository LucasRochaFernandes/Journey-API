using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases;
public class GetByIdUseCase
{
    public ResponseTripJson Execute(Guid tripId)
    {
        var dbContext = new JourneyDbContext();
        var trip = dbContext
            .Trips
            .Include(t => t.Activities)
            .FirstOrDefault(t => t.Id.Equals(tripId));

        if (trip is null)
        {
            throw new JourneyException("Trip Not Found");
        }

        return new ResponseTripJson
        {
            Id = trip.Id,
            Name = trip.Name,
            EndDate = trip.EndDate,
            StartDate = trip.StartDate,
            Activities = trip.Activities.Select(activity => new ResponseActivityJson
            {
                Id = activity.Id,
                Name = activity.Name,
                Date = activity.Date,
                Status = (Communication.Enums.ActivityStatus)activity.Status,
            }).ToList(),
        };
    }
}
