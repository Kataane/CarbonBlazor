using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class Dropdown : BaseComponent
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Helper { get; set; }

        [Parameter]
        public IEnumerable<string> Items { get; set; } = null!;

        [Parameter]
        public string Size { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (!string.IsNullOrEmpty(Label)) return;

            Label = Items.First();
        }
    }
}