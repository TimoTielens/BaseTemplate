using AppointMe.Shared.Jobs;

namespace AppointMe.Api.Hangfire;

internal sealed class RecurringJobsHostedService(
    IRecurringJobScheduler scheduler,
    IEnumerable<IRecurringJobRegistrar> registrars
) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var registrar in registrars)
        {
            registrar.Register(scheduler);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
