using AppointMe.Booking.Attendees;
using AppointMe.Booking.Contracts.Appointments.Events;
using AppointMe.Booking.ServiceProviders;

namespace AppointMe.Booking.Appointments.ScheduleAppointment;

public static class ScheduleAppointment
{
    extension(Appointment)
    {
        public static Appointment Schedule(
            CompanyId companyId,
            ServiceProviderId providerId,
            AttendeeId attendeeId,
            DateTimeOffsetPeriod period,
            LongString? notes,
            DateTimeOffset scheduledAt)
        {
            var appointment = new Appointment
            {
                Id = new AppointmentId(NewId()),
                CompanyId = companyId,
                Period = period,
                ProviderId = providerId,
                AttendeeId = attendeeId,
                Notes = notes,
                Status = AppointmentStatus.Scheduled,
                ScheduledAt = scheduledAt
            };

            var appointmentScheduledEvent = new AppointmentScheduledEvent(appointment.Id.Value, companyId.Value,
                providerId.Value, attendeeId.Value, period.Start, period.End, notes?.Value);
            appointment.Raise(appointmentScheduledEvent);

            return appointment;
        }
    }
}
