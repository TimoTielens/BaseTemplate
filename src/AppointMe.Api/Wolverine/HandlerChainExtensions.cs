using System.Reflection;
using Wolverine.Runtime.Handlers;

namespace AppointMe.Api.Wolverine;

public static class HandlerChainExtensions
{
    extension(HandlerChain chain)
    {
        public bool Uses<T>()
        {
            return chain.Handlers.Any(handler =>
                HasParameter(handler.Method.GetParameters())
                || HasParameter(handler.HandlerType.GetConstructors()
                    .SelectMany(constructor => constructor.GetParameters()))
            );

            bool HasParameter(IEnumerable<ParameterInfo> parameters)
            {
                return parameters.Any(parameterInfo =>
                    parameterInfo.ParameterType == typeof(T));
            }
        }
    }
}
