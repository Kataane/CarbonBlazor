using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class SelectItemGroup : BaseComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Label { get; set; }
    }
}