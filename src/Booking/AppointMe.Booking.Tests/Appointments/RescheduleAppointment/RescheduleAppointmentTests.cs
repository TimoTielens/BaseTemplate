using System.Globalization;
using AppointMe.Booking.Appointments.RescheduleAppointment;
using AppointMe.Booking.Contracts.Appointments.Events;

namespace AppointMe.Booking.Tests.Appointments.RescheduleAppointment;

public class RescheduleAppointmentTests
{
    private static readonly DateTimeOffset Anchor =
        DateTimeOffset.Parse("5 Oct 2025 12:00Z", DateTimeFormatInfo.InvariantInfo);

    [Fact]
    public void should_update_provider_and_period_and_raise_event()
    {
        var appointment = Create.Appointment.Build();
        var originalCompanyId = appointment.CompanyId;
        var originalAttendeeId = appointment.AttendeeId;
        var originalNotes = appointment.Notes;
        var originalScheduledAt = appointment.ScheduledAt;

        var newProviderId = new ServiceProviderId(NewId());
        var newPeriod = DateTimeOffsetPeriod.Create(Anchor.AddDays(2), Anchor.AddDays(2).AddHours(1));

        appointment.Reschedule(newProviderId, newPeriod);

        Assert.Equal(newProviderId, appointment.ProviderId);
        Assert.Equal(newPeriod, appointment.Period);
        Assert.Equal(AppointmentStatus.Scheduled, appointment.Status);
        Assert.Equal(originalCompanyId, appointment.CompanyId);
        Assert.Equal(originalAttendeeId, appointment.AttendeeId);
        Assert.Equal(originalNotes, appointment.Notes);
        Assert.Equal(originalScheduledAt, appointment.ScheduledAt);

        var @event = appointment.Events.OfType<AppointmentRescheduledEvent>().Single();
        Assert.Equal(appointment.Id.Value, @event.AppointmentId);
        Assert.Equal(originalCompanyId.Value, @event.CompanyId);
        Assert.Equal(newProviderId.Value, @event.ProviderId);
        Assert.Equal(newPeriod.Start, @event.Start);
        Assert.Equal(newPeriod.End, @event.End);
    }

    [Fact]
    public void should_throw_when_rescheduling_a_cancelled_appointment()
    {
        var appointment = Create.Appointment.WithStatus(AppointmentStatus.Cancelled).Build();
        var originalProviderId = appointment.ProviderId;
        var originalPeriod = appointment.Period;

        var newProviderId = new ServiceProviderId(NewId());
        var newPeriod = DateTimeOffsetPeriod.Create(Anchor.AddDays(2), Anchor.AddDays(2).AddHours(1));

        Assert.Throws<ValidationException>(() => appointment.Reschedule(newProviderId, newPeriod));

        Assert.Equal(originalProviderId, appointment.ProviderId);
        Assert.Equal(originalPeriod, appointment.Period);
        Assert.Empty(appointment.Events);
    }
}
