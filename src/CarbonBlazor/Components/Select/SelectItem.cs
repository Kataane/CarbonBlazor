using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class SelectItem : BaseComponent
    {
        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public string Value { get; set; }

        [CascadingParameter(Name="parent")]
        public Select Parent { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}