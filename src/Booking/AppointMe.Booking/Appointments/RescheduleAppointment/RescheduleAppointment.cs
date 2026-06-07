using AppointMe.Booking.Contracts.Appointments.Events;
using AppointMe.Booking.ServiceProviders;

namespace AppointMe.Booking.Appointments.RescheduleAppointment;

public static class RescheduleAppointment
{
    extension(Appointment appointment)
    {
        public void Reschedule(ServiceProviderId providerId, DateTimeOffsetPeriod period)
        {
            if (appointment.Status != AppointmentStatus.Scheduled)
            {
                throw new ValidationException("Only scheduled appointments can be rescheduled.");
            }

            appointment.ProviderId = providerId;
            appointment.Period = period;

            var appointmentRescheduledEvent = new AppointmentRescheduledEvent(appointment.Id.Value,
                appointment.CompanyId.Value,
                providerId.Value,
                period.Start,
                period.End);
            appointment.Raise(appointmentRescheduledEvent);
        }
    }
}
