using JasperFx;
using JasperFx.CodeGeneration;
using JasperFx.CodeGeneration.Frames;
using Wolverine.Configuration;
using Wolverine.Runtime.Handlers;

namespace AppointMe.Api.Wolverine.HandlerContext;

public class CompanyContextPolicy : IHandlerPolicy
{
    public void Apply(IReadOnlyList<HandlerChain> chains, GenerationRules rules, IServiceContainer container)
    {
        foreach (var chain in chains.Where(chain => chain.Uses<CompanyId>()))
        {
            chain.Middleware.Add(new MethodCall(typeof(CompanyContextBehavior),
                nameof(CompanyContextBehavior.Load)));
        }
    }
}
