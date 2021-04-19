using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class Select
    {
        [Parameter] 
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Value { get; set; }
        
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }   

        [Parameter]
        public EventCallback<ChangeEventArgs> OnChange { get; set; }
    }
}