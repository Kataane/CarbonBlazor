using System.Threading.Tasks;
using CarbonBlazor.Utilities;
using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class RadioButtonGroup : BaseComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Group { get; set; }

        [Parameter]
        public Orientation Orientation
        {
            set { _orientation = value.ToString().ToLower(); }
        }

        [Parameter]
        public string Value { get; set; }

        [Parameter] 
        public EventCallback<string> ValueChanged { get; set; }

        private RadioButton _radioButton = new RadioButton();

        private string _orientation;

        private IdGenerator idGenerator = new IdGenerator();

        protected override void OnInitialized()
        {
            Group = idGenerator.Generate;

            base.OnInitialized();
        }

        public void Update(RadioButton radioButton)
        {
            _radioButton = radioButton;
        }

        public void RadioButtonChange()
        {
            if (!ValueChanged.HasDelegate) return;
            Task.Run(() => ValueChanged.InvokeAsync(_radioButton.Value));
        }
    }
}