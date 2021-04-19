using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class CheckboxGroup
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public IEnumerable<string> Values { get; set; }

        [Parameter]
        public EventCallback<IEnumerable<string>> ValuesChanged { get; set; }

        private List<Checkbox> checkboxs = new List<Checkbox>();

        public void Add(Checkbox checkbox)
        {
            if (checkboxs.Contains(checkbox)) return;
            checkboxs.Add(checkbox);
        }

        public void Remove(Checkbox checkbox)
        {
            if (!checkboxs.Contains(checkbox)) return;
            checkboxs.Remove(checkbox);
        }

        public async Task CheckBoxChange()
        {
            //TODO: Select only values is checked
            Values = checkboxs.Select(c => c.Value).ToList();

            if (!ValuesChanged.HasDelegate) return;
            await ValuesChanged.InvokeAsync(Values);
        }
    }
}