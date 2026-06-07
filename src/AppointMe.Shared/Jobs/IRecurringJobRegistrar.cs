namespace AppointMe.Shared.Jobs;

public interface IRecurringJobRegistrar
{
    void Register(IRecurringJobScheduler scheduler);
}
