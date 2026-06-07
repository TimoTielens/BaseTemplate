using System.Reflection;

namespace AppointMe.Identity;

public static class ModuleAssembly
{
    public static Assembly Instance => typeof(ModuleAssembly).Assembly;
}
