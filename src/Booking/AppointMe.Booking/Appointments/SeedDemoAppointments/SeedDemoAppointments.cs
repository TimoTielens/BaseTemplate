using AppointMe.Booking.Appointments.ScheduleAppointment;
using AppointMe.Booking.Attendees;
using AppointMe.Booking.ServiceProviders;
using Bogus;

namespace AppointMe.Booking.Appointments.SeedDemoAppointments;

public sealed class SeedDemoAppointments(TimeProvider timeProvider)
{
    public IEnumerable<Appointment> Generate(
        CompanyId companyId,
        AttendeeId attendeeId,
        IList<ServiceProviderId> providerIds,
        int count)
    {
        if (providerIds.Count == 0 || count <= 0)
        {
            return [];
        }

        var faker = new Faker();
        var now = timeProvider.GetUtcNow();
        var today = new DateTimeOffset(now.Year, now.Month, now.Day, 0, 0, 0, TimeSpan.Zero);

        return Enumerable.Range(0, count * 4)
            .Select(_ => (
                ProviderId: faker.Random.ListItem(providerIds),
                Start: today.AddDays(faker.Random.Int(-21, 21)).AddHours(faker.Random.Int(8, 19))))
            .Where(slot => slot.Start.DayOfWeek is not DayOfWeek.Sunday)
            .DistinctBy(slot => (slot.ProviderId.Value, slot.Start))
            .Take(count)
            .Select(slot => Appointment.Schedule(
                companyId: companyId,
                providerId: slot.ProviderId,
                attendeeId: attendeeId,
                period: DateTimeOffsetPeriod.Create(
                    slot.Start,
                    slot.Start.Add(faker.Random.Bool(0.08f) ? TimeSpan.FromHours(1) : TimeSpan.FromMinutes(30))),
                notes: faker.Random.Bool(0.08f) ? LongString.Create(faker.Lorem.Sentence()) : null,
                scheduledAt: now));
    }
}
