using SchedulePlanner.Domain.Enums;

namespace SchedulePlanner.Domain.EventAttributes.Attributes;

public class RecurrenceEventAttribute : EventAttribute
{
    public RecurrenceType? Type { get; private set; }

    public DateTime? Until { get; }
    
    public RecurrenceEventAttribute(bool isActive, RecurrenceType type, DateTime until) : base(isActive)
    {
        Type = type;
        Until = until;
    }

    private RecurrenceEventAttribute() { }
    
    public override string Description => "Повторяемое событие";
    
    protected override IReadOnlyCollection<FieldMetadata> GetAttributeFieldsMetadata()
    {
        return new List<FieldMetadata>
        {
            new()
            {
                FieldName = nameof(Type),
                FieldType = FieldTypes.String,
                DefaultValue = RecurrenceType.EveryWeek.ToString(),
                Description = "Повторяемость",
                PossibleChoices =
                [
                    RecurrenceType.EveryDay.ToString(),
                    RecurrenceType.EveryWeek.ToString(),
                    RecurrenceType.EveryMonth.ToString()
                ]
            },
            new()
            {
                FieldName = nameof(Until),
                FieldType = FieldTypes.DateTime,
                Description = "Повторять до"
            }
        };
    }
}