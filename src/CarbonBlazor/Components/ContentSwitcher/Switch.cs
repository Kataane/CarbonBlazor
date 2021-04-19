using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class Switch : BaseComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Label { get; set; }

        [CascadingParameter(Name = "parent")]
        public ContentSwitcher Parent { get; set; }

        [Parameter]
        public bool Selected { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Parent?.AddSwitch(this);

            if (Selected) Parent?.Select(Id);
        }

        private void Select()
        {
            if (Selected) return;
            Parent?.Select(Id);
        }
    }
}