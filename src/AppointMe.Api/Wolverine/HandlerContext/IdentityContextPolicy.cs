using JasperFx;
using JasperFx.CodeGeneration;
using JasperFx.CodeGeneration.Frames;
using Wolverine.Configuration;
using Wolverine.Runtime.Handlers;

namespace AppointMe.Api.Wolverine.HandlerContext;

public class IdentityContextPolicy : IHandlerPolicy
{
    public void Apply(IReadOnlyList<HandlerChain> chains, GenerationRules rules, IServiceContainer container)
    {
        foreach (var chain in chains.Where(chain => chain.Uses<Shared.Authentication.IIdentity>()))
        {
            chain.Middleware.Add(
                new MethodCall(typeof(IdentityContextBehavior), nameof(IdentityContextBehavior.Load)));
        }
    }
}
