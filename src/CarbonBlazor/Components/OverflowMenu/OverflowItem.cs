using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CarbonBlazor
{
    public partial class OverflowItem : BaseComponent
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public bool HasDivider { get; set; }

        [Parameter]
        public bool IsDanger { get; set; }

        [Parameter]
        public string Name { get; set; }

        [CascadingParameter(Name = "parent")]
        private OverflowMenu _parent { get; set; }

        protected override void SetClass()
        {
            _mapper
                .Clear()
                .SetBase("cb--overflow-menu-options__option")
                .Add("cb--overflow-menu--divider", HasDivider)
                .Add("cb--overflow-menu-options__option--danger", IsDanger)
                .Add("cb--overflow-menu-options__option--disabled", Disabled)
                .Add(Class);
        }

        private async Task OnItemClick()
        {
            if (OnClick.HasDelegate) await OnClick.InvokeAsync();

            if (_parent.SelectedValueChanged.HasDelegate)
                await _parent?.SelectedValueChanged.InvokeAsync(Value);
        }
    }
}