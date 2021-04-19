using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class Accordion : BaseComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public Size Size { get; set; }

        [Parameter]
        public Align Align { get; set; } = Align.End;

        protected override void SetClass()
        {
            _mapper
                .Clear()
                .SetBase("cb--accordion")
                .Add("cb--accordion--" + Size.GetDescription(), () => Size > Size.Field)
                .Add("cb--accordion--" + Align.GetDescription())
                .Add(Class);

            _class = _mapper.Class;
        }
    }
}