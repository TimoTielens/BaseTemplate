using AppointMe.Shared.Database.Dapper.TypeHandlers;
using Dapper;

namespace AppointMe.Shared.Database.Dapper;

public static class DapperTypeHandlerRegistration
{
    public static void Register()
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new UtcDateTimeOffsetTypeHandler());
    }
}
