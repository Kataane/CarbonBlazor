using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CarbonBlazor
{
    public partial class Button : BaseComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public ButtonKind Kind { get; set; }

        [Parameter]
        public Size Size { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public bool OnlyIcon { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected override void SetClass()
        {
            _mapper
                .Clear()
                .SetBase("cb--btn")
                .Add(Kind.GetDescription())
                .Add("cb--btn--disabled", Disabled)
                .Add("cb--btn--" + Size.GetDescription(), () => Size != Size.Primary)
                .Add("cb--btn--icon-only", () => OnlyIcon)
                .Add(Class);

            _class = _mapper.Class;
        }
    }
}