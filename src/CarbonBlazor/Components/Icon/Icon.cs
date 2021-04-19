using System;
using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class Icon
    {
        [Parameter]
        public int Size { get; set; } = 16;

        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public string Class { get; set; }

        private string _path;

        private string _title;

        protected override void OnParametersSet()
        {
            if(!Enum.IsDefined(typeof(IconSize), Size)) return;

            //? Need check is path? For security. Maybe exist XSS or somthing else?
            // if(Type == "<path>") return;
            
            _path = Type;
        }
    }
}