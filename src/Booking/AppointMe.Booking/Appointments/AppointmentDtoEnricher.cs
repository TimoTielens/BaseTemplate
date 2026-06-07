namespace AppointMe.Booking.Appointments;

internal static class AppointmentDtoEnricher
{
    extension(AppointmentDto appointment)
    {
        public AppointmentDto Enrich()
        {
            var providerName = PersonName.CreateOrNull(appointment.ProviderFirstName, appointment.ProviderLastName);
            var attendeeName = PersonName.CreateOrNull(appointment.AttendeeFirstName, appointment.AttendeeLastName);
            return appointment with
            {
                ProviderName = providerName?.FullName,
                ProviderInitials = providerName?.Initials,
                AttendeeName = attendeeName?.FullName,
                AttendeeInitials = attendeeName?.Initials
            };
        }
    }
}
