using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CarbonBlazor
{
    public partial class Tag : BaseComponent
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public TagType Type { get; set; }

        [Parameter]
        public Size Size { get; set; }

        [Parameter]
        public TagColor Color { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public Action OnClose { get; set; }

        protected override void SetClass()
        {
            _mapper
                .Clear()
                .SetBase("cb--tag")
                .Add("cb--tag--" + Type.ToString().ToLower(), () => Type == TagType.Filter)
                .Add("cb--tag--disabled", Disabled)
                .Add("cb--tag--" + Color.GetDescription())
                .Add("cb--tag--" + Size.GetDescription(), Size == Size.Sm || Size == Size.Md)
                .Add(Class);

            _class = _mapper.Class;
        }
    }
}