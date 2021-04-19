using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CarbonBlazor
{
    public partial class AccordionItem : BaseComponent
    {

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public bool Open { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected override void SetClass()
        {
            _mapper
                .Clear()
                .SetBase("cb--accordion__item")
                .Add("cb--accordion__item--active", Open)
                .Add("cb--accordion__item--disabled", Disabled)
                .Add(Class);
        }
    }
}