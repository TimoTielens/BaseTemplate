using AppointMe.Shared.Domain.Errors;
using Wolverine;

namespace AppointMe.Api.Wolverine.HandlerContext;

public static class CompanyContextBehavior
{
    public static CompanyId Load(
        ICurrentCompany currentCompany,
        IMessageContext messageContext)
    {
        var companyId = currentCompany.CompanyId
                        ?? throw new AccessDeniedException("Active company was not specified.");
        messageContext.TenantId = companyId.Value.ToString();
        return companyId;
    }
}
