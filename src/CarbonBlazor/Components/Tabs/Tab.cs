using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class Tab : BaseComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Label { get; set; }

        [CascadingParameter(Name = "parent")]
        public Tabs Parent { get; set; }

        [Parameter]
        public bool Selected { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Parent?.AddTab(this);

            if (Selected) Parent?.Select(Id);
        }

        protected override void SetClass()
        {
            _mapper
                .Clear()
                .SetBase("cb--tabs__nav-item")
                .Add("cb--tabs__nav-item--disabled", Disabled)
                .Add("cb--tabs__nav-item--selected", Selected)
                .Add(Class);
        }

        private void Select()
        {
            if (Selected) return;
            Parent?.Select(Id);
        }
    }
}