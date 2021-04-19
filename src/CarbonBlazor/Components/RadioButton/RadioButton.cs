using System;
using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class RadioButton : InputBase<bool>
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public bool Checked { get; set; }

        public EventCallback<bool> CheckedChanged;

        [CascadingParameter (Name = "Parent")] 
        protected RadioButtonGroup RadioButtonGroup { get; set; }

        protected string Group => RadioButtonGroup?.Group ?? Id;

        protected override void OnInitialized() 
        {
            base.OnInitialized();

            if (!Checked) return;
            InputChange(new ChangeEventArgs{Value = "on"});
        }

        protected void InputChange(ChangeEventArgs args)
        {
            if (args is null || !(args.Value is string vs)) return;

            var value = vs == "on" ? true : false;

            CurrentValue = value;

            RadioButtonGroup?.Update(this);
            InnerCheckedChange();
        }

        private void InnerCheckedChange()
        {
            if (CheckedChanged.HasDelegate) CheckedChanged.InvokeAsync();
            RadioButtonGroup?.RadioButtonChange();
        }
    }
}