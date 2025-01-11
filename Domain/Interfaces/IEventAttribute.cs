using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Domain.Interfaces;

public interface IEventAttribute
{
    bool IsActive { get; }
    
    string GetDescription();

    IReadOnlyCollection<FieldMetadata> GetFieldsMetadata();
}