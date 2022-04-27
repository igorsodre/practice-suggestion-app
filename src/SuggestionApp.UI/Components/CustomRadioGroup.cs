using Microsoft.AspNetCore.Components.Forms;

namespace SuggestionApp.UI.Components;

public class CustomRadioGroup<TValue> : InputRadioGroup<TValue>
{
    private string _name = string.Empty;
    private string _fieldClass = string.Empty;

    protected override void OnParametersSet()
    {
        var fieldClass = EditContext?.FieldCssClass(FieldIdentifier) ?? string.Empty;
        if (fieldClass == _fieldClass && Name == _name)
            return;

        _fieldClass = fieldClass;
        _name = Name ?? string.Empty;

        base.OnParametersSet();
    }
}
