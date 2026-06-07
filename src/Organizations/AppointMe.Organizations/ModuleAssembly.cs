using System.Reflection;

namespace AppointMe.Organizations;

public static class ModuleAssembly
{
    public static Assembly Instance => typeof(ModuleAssembly).Assembly;
}
