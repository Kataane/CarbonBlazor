using System;
using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class Slider : BaseComponent
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public int Max { get; set; } = 100;

        [Parameter]
        public int Min { get; set; } = 0;

        [Parameter]
        public int Step { get; set; } = 1;

        [Parameter]
        public int StepMuliplier { get; set; } = 4;

        [Parameter]
        public int Value { get; set; } = 0;
    }
}