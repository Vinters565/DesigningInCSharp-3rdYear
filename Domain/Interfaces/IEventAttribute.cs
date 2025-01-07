using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Domain.Interfaces;

public interface IEventAttribute
{
    string Description { get; }
    
    bool IsActive { get; }

    IReadOnlyCollection<FieldMetadata> GetFieldsMetadata();
}