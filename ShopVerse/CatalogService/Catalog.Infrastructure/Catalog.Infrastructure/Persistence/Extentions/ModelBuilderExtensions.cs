using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class ModelBuilderExtensions
{
    public static PropertyBuilder<TValueObject> HasGuidConversion<TValueObject>(
        this PropertyBuilder<TValueObject> builder,
        Func<Guid, TValueObject> fromGuid,
        Func<TValueObject, Guid> toGuid)
    {
        return builder.HasConversion(new ValueConverter<TValueObject, Guid>(
            x => toGuid(x),  
            x => fromGuid(x)
        ));
    }
}
