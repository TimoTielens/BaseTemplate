using System.Linq.Expressions;
using AppointMe.Shared.Jobs;
using Hangfire;

namespace AppointMe.Api.Hangfire;

internal sealed class HangfireRecurringJobScheduler(IRecurringJobManager manager) : IRecurringJobScheduler
{
    public void AddOrUpdate<TJob>(
        string jobId,
        Expression<Func<TJob, Task>> methodCall,
        string cronExpression)
    {
        manager.AddOrUpdate(jobId, methodCall, cronExpression);
    }
}
