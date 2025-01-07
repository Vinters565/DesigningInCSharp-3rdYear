using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Domain.Interfaces;

public interface IEventAttribute
{
    string Name { get; }
    
    bool IsActive { get; }

    IReadOnlyCollection<FieldMetadata> GetFieldsMetadata();
}