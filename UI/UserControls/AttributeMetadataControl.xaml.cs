using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using UI.Dto;

namespace UI.UserControls;

public partial class AttributeMetadataControl : UserControl
{
    private readonly Dictionary<string, Func<object>> atributeFields = new();
    public string AttributeName
    {
        get => FieldName.Text;
        set => FieldName.Text = value;
    }

    public List<Tuple<string, object>> GetValues => atributeFields
        .Select(element => new Tuple<string, object>(element.Key, element.Value()))
        .ToList();

    public AttributeMetadataControl(AttributeMetadata attributeMetadata)
    {
        InitializeComponent();
        AttributeName = attributeMetadata.Description;
        foreach (var field in attributeMetadata.Fields)
        {
            var element = AttributeFormGenerator.GenerateFieldControl(field);
            Container.Children.Add(element.Item1);
            atributeFields.Add(field.FieldName, element.Item2);
        }
    }
}
