using CarbonBlazor.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CarbonBlazor
{
    public partial class OverflowMenu : BaseComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string SelectedValue { get; set; }

        [Parameter]
        public EventCallback<string> SelectedValueChanged { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public bool Name { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public bool Open { get; set; }

        [Parameter]
        public Size Size { get; set; }

        private ClassMapper _menuOptionsClasses = new ClassMapper();

        protected override void SetClass()
        {
            _mapper
                .Clear()
                .SetBase("cb--overflow-menu")
                .Add("cb--overflow-menu--open", Open)
                .Add("cb--overflow-menu--light", Light)
                .Add("cb--overflow-menu--" + Size.GetDescription(), Size > Size.Field)
                .Add(Class);

            _menuOptionsClasses
                .Clear()
                .SetBase("cb--overflow-menu-options")
                .Add("cb--overflow-menu-options--open", Open)
                .Add("cb--overflow-menu-options--light", Light)
                .Add("cb--overflow-menu-options--" + Size.GetDescription(), Size > Size.Field);
        }
    }
}