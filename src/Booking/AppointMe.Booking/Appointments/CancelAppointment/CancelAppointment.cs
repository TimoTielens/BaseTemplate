using AppointMe.Booking.Contracts.Appointments.Events;

namespace AppointMe.Booking.Appointments.CancelAppointment;

public static class CancelAppointment
{
    extension(Appointment appointment)
    {
        public void Cancel()
        {
            if (appointment.Status != AppointmentStatus.Scheduled)
            {
                throw new ValidationException("Only scheduled appointments can be cancelled.");
            }

            appointment.Status = AppointmentStatus.Cancelled;

            appointment.Raise(new AppointmentCancelledEvent(appointment.Id.Value, appointment.CompanyId.Value));
        }
    }
}
