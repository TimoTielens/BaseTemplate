using AppointMe.Organizations.Contracts.Employees.Events;

namespace AppointMe.Organizations.Employees.DeleteEmployee;

public static class DeleteEmployee
{
    extension(Employee employee)
    {
        public void Delete()
        {
            employee.IsDeleted = true;
            employee.Raise(new EmployeeDeleted(employee.Id.Value, employee.CompanyId.Value));
        }
    }
}
