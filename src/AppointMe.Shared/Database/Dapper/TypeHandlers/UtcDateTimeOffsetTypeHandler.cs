using System.Data;
using Dapper;

namespace AppointMe.Shared.Database.Dapper.TypeHandlers;

public sealed class UtcDateTimeOffsetTypeHandler : SqlMapper.TypeHandler<DateTimeOffset>
{
    public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
        => parameter.Value = value.UtcDateTime;

    // Columns may be `datetime2` (Booking module, via EF value converter) or `datetimeoffset`
    // (every other module, mapped natively). Dapper boxes them as DateTime or DateTimeOffset
    // respectively; normalize both to a UTC-offset DateTimeOffset.
    public override DateTimeOffset Parse(object value) => value switch
    {
        DateTimeOffset dateTimeOffset => dateTimeOffset.ToUniversalTime(),
        DateTime dateTime => new DateTimeOffset(dateTime, TimeSpan.Zero),
        _ => throw new InvalidCastException(
            $"Cannot convert {value.GetType().FullName} to {nameof(DateTimeOffset)}.")
    };
}
