namespace AppointMe.Shared.Tests;

public abstract class Specification
{
    protected Specification()
    {
        // ReSharper disable VirtualMemberCallInConstructor
        Context();
        Because();
        // ReSharper restore VirtualMemberCallInConstructor
    }

    protected virtual void Context()
    {
    }

    protected abstract void Because();
}
