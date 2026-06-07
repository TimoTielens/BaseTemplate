namespace AppointMe.Organizations.Employees.DeleteEmployee;

internal sealed class DeleteEmployeeEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/employees/{id:guid}", DeleteEmployee)
            .WithName(nameof(DeleteEmployee));
    }

    private static async Task DeleteEmployee(Guid id, IMessageBus bus, CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(new DeleteEmployeeCommand { Id = id }, cancellationToken);
    }
}
