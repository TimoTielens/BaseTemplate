using AppointMe.Booking.Appointments.CancelAppointment;
using AppointMe.Booking.Contracts.Appointments.Events;

namespace AppointMe.Booking.Tests.Appointments.CancelAppointment;

public class CancelAppointmentTests
{
    [Fact]
    public void should_set_status_to_cancelled_and_raise_event()
    {
        var appointment = Create.Appointment.Build();

        appointment.Cancel();

        Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);

        var @event = appointment.Events.OfType<AppointmentCancelledEvent>().Single();
        Assert.Equal(appointment.Id.Value, @event.AppointmentId);
        Assert.Equal(appointment.CompanyId.Value, @event.CompanyId);
    }

    [Fact]
    public void should_throw_when_cancelling_an_already_cancelled_appointment()
    {
        var appointment = Create.Appointment.WithStatus(AppointmentStatus.Cancelled).Build();

        Assert.Throws<ValidationException>(appointment.Cancel);

        Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);
        Assert.Empty(appointment.Events);
    }
}
