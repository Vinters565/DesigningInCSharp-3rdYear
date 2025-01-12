using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Domain.ValueTypes;

namespace SchedulePlanner.Infrastructure.ValueConverters;

public class AttributeDataValueConverter : ValueConverter<AttributeData, string>
{
    public AttributeDataValueConverter(JsonSerializerOptions options) 
        : base(
            attributeData => JsonSerializer.Serialize(attributeData.Attributes, options),
            v => new AttributeData(JsonSerializer.Deserialize<Dictionary<Type, IEventAttribute>>(v, options)))
    {
    }
}