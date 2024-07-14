using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases;
public class DeleteTripUseCase
{
    public void Execute(Guid tripId)
    {
        var dbContext = new JourneyDbContext();
        var trip = dbContext.Trips
            .Include(trip => trip.Activities)
            .FirstOrDefault(trip => trip.Id.Equals(tripId));
        if (trip is null) {
            throw new NotFoundException("Trip Not Found");
        }
        dbContext.Trips.Remove(trip);
        dbContext.SaveChanges();
    }
}
