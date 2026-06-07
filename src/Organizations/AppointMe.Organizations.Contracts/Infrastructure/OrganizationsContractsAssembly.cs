using System.Reflection;
using Wolverine.Attributes;

[assembly: WolverineModule]

namespace AppointMe.Organizations.Contracts.Infrastructure;

public static class OrganizationsContractsAssembly
{
    public static Assembly Instance => typeof(OrganizationsContractsAssembly).Assembly;
}
