using System.Text.Json.Serialization;

namespace AppointMe.Booking.Appointments;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AppointmentStatus
{
    Scheduled,
    Cancelled
}
