using System.Threading.Tasks;
using CarbonBlazor.Utilities;
using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public abstract class BaseComponent : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// Specify whether the Component should be disabled, or not.
        /// </summary>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Specify an optional className to be added to Component.
        /// </summary>
        [Parameter]
        public string Class { get; set; }

        [Parameter]
        public string Selectors { get; set; }

        private IdGenerator idGenerator = new IdGenerator();

        protected ClassMapper _mapper = new ClassMapper();

        protected string _class;

        protected override void OnInitialized()
        {
            if (string.IsNullOrEmpty(Id)) Id = idGenerator.Generate;
            
            base.OnInitialized();
        }

        protected override async Task OnParametersSetAsync()
        {
            base.OnParametersSet();

            await Task.Run(() => SetClass());
        }

        protected virtual void SetClass(){}
    }
}