using System.Linq.Expressions;

namespace AppointMe.Shared.Jobs;

public interface IRecurringJobScheduler
{
    void AddOrUpdate<TJob>(
        string jobId,
        Expression<Func<TJob, Task>> methodCall,
        string cronExpression);
}
