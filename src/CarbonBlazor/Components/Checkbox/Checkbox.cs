using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class Checkbox : InputBase<bool>
    {
        [Parameter]
        public bool Checked
        {
            get => CurrentValue;
            set
            {
                if (CurrentValue != value)
                {
                    CurrentValue = value;
                }
            }
        }

        [Parameter]
        public EventCallback<bool> CheckedChanged { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool Indeterminate { get; set; }

        [CascadingParameter]
        public CheckboxGroup CheckboxGroup { get; set; }


        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();

            if (!Checked) return;
            CheckboxGroup?.Add(this);
            await InnerCheckedChange(Checked);
        }

        protected async Task InputChange(ChangeEventArgs args)
        {
            if (args != null && args.Value is bool @checked)
            {
                if (@checked) CheckboxGroup?.Add(this);
                else CheckboxGroup?.Remove(this);

                await Task.Run(() => CurrentValue = @checked)
                .ContinueWith(_ => InnerCheckedChange(@checked));
            }
        }

        protected async Task InnerCheckedChange(bool @checked)
        {
            if (CheckedChanged.HasDelegate)
            {
                await CheckedChanged.InvokeAsync(@checked);
            }

            CheckboxGroup?.CheckBoxChange();
        }
    }
}