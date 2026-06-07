using System.Reflection;

namespace AppointMe.Crm;

public static class ModuleAssembly
{
    public static Assembly Instance => typeof(ModuleAssembly).Assembly;
}
