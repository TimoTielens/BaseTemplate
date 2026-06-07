namespace AppointMe.Shared.Domain;

public static class Id
{
    public static Guid NewId() => Guid.CreateVersion7();
}
