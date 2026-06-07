using AppointMe.Shared.Domain;
using JasperFx;
using JasperFx.CodeGeneration;
using JasperFx.CodeGeneration.Frames;
using Wolverine.Configuration;
using Wolverine.Runtime.Handlers;

namespace AppointMe.Api.Wolverine.HandlerContext;

public class DomainEventContextPolicy : IHandlerPolicy
{
    public void Apply(IReadOnlyList<HandlerChain> chains, GenerationRules rules, IServiceContainer container)
    {
        foreach (var chain in chains
                     .Where(handlerChain => handlerChain.MessageType.IsAssignableTo(typeof(IDomainEvent))))
        {
            chain.Middleware.Insert(0, new MethodCall(typeof(SystemContextBehavior),
                nameof(SystemContextBehavior.Apply)));
        }
    }
}
