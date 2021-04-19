using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class TextInput : InputBase<string>
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Helper { get; set; }

        [Parameter]
        public Size Size { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public bool Invalid { get; set; }

        private string _inputWrapperClasses;

        protected override void SetClass()
        {
            _class = _mapper
                .Clear()
                .SetBase("cb--text-input")
                .Add("cb--text-input--light", Light)
                .Add("cb--text-input--invalid", Invalid)
                .Add("cb--" + Size.GetDescription(), () => Size > Size.Field)
                .Add(Class)
                .Class;
        }
    }
}