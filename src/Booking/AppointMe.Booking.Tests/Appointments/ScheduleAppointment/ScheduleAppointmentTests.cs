using System.Globalization;
using AppointMe.Booking.Appointments.ScheduleAppointment;
using AppointMe.Booking.Contracts.Appointments.Events;

namespace AppointMe.Booking.Tests.Appointments.ScheduleAppointment;

public class ScheduleAppointmentTests
{
    private static readonly DateTimeOffset Anchor =
        DateTimeOffset.Parse("5 Oct 2025 12:00Z", DateTimeFormatInfo.InvariantInfo);

    [Fact]
    public void should_schedule_appointment_with_initial_state_and_raise_event()
    {
        var companyId = new CompanyId(NewId());
        var providerId = new ServiceProviderId(NewId());
        var attendeeId = new AttendeeId(NewId());
        var period = DateTimeOffsetPeriod.Create(Anchor, Anchor.AddHours(1));
        var notes = LongString.Create("First consultation");
        var scheduledAt = Anchor.AddDays(-1);

        var appointment = Appointment.Schedule(companyId, providerId, attendeeId, period, notes, scheduledAt);

        Assert.NotEqual(Guid.Empty, appointment.Id.Value);
        Assert.Equal(companyId, appointment.CompanyId);
        Assert.Equal(providerId, appointment.ProviderId);
        Assert.Equal(attendeeId, appointment.AttendeeId);
        Assert.Equal(period, appointment.Period);
        Assert.Equal(notes, appointment.Notes);
        Assert.Equal(AppointmentStatus.Scheduled, appointment.Status);
        Assert.Equal(scheduledAt, appointment.ScheduledAt);

        var @event = appointment.Events.OfType<AppointmentScheduledEvent>().Single();
        Assert.Equal(appointment.Id.Value, @event.AppointmentId);
        Assert.Equal(companyId.Value, @event.CompanyId);
        Assert.Equal(providerId.Value, @event.ProviderId);
        Assert.Equal(attendeeId.Value, @event.AttendeeId);
        Assert.Equal(period.Start, @event.Start);
        Assert.Equal(period.End, @event.End);
        Assert.Equal(notes.Value, @event.Notes);
    }

    [Fact]
    public void should_carry_null_notes_into_event_when_notes_are_not_provided()
    {
        var companyId = new CompanyId(NewId());
        var providerId = new ServiceProviderId(NewId());
        var attendeeId = new AttendeeId(NewId());
        var period = DateTimeOffsetPeriod.Create(Anchor, Anchor.AddHours(1));

        var appointment = Appointment.Schedule(companyId, providerId, attendeeId, period, notes: null, scheduledAt: Anchor);

        Assert.Null(appointment.Notes);

        var @event = appointment.Events.OfType<AppointmentScheduledEvent>().Single();
        Assert.Null(@event.Notes);
    }
}
