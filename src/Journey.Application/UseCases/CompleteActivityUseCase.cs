using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Enums;

namespace Journey.Application.UseCases;
public class CompleteActivityUseCase
{
    public void Execute(Guid activityId)
    {
        var dbContext = new JourneyDbContext();
        var activity = dbContext.Activities.FirstOrDefault(activity => activity.Id.Equals(activityId));
        if(activity is null)
        {
            throw new NotFoundException("Activity Not Found");
        }
        activity.Status = ActivityStatus.Done;
        dbContext.Activities.Update(activity);
        dbContext.SaveChanges();
    }
}
