using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarbonBlazor
{
    public partial class ContentSwitcher : BaseComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public int Selected {get ; set; } = -1;

        [Parameter]
        public EventCallback<int> SelectedChanged { get; set; }

        private List<Switch> _switchers = new List<Switch>();

        private RenderFragment _selected;

        public void AddSwitch(Switch @switch)
        {
            _switchers.Add(@switch);

            //? Premature optimization. 
            //? If alredy tab witch content selected skip all this part
            if (!(_selected is null)) return;

            if (Selected == -1) 
            {
                Select(_switchers[0].Id);
                return;
            }

            if (Selected != -1 && _switchers.Count == Selected)
            {
                Select(@switch.Id);
                return;
            }

            //TODO: When Selected out of range. Set Selected as first element
        }

        public void Select(string id)
        {
            if (!_switchers.Exists(s => string.Equals(s.Id, id))) return;

            var sw = _switchers.FirstOrDefault(s => s.Selected == true);
            if (!(sw is null)) sw.Selected = false;

            var @switch = _switchers.FirstOrDefault(s => string.Equals(s.Id, id));
            @switch.Selected = true;
            _selected = @switch.ChildContent;
            Selected = _switchers.IndexOf(@switch);
            
            StateHasChanged();
            InnerSelectChange();
        }

        private void InnerSelectChange()
        {
            if (!SelectedChanged.HasDelegate) return;
            Task.Run(() => SelectedChanged.InvokeAsync(Selected));
        }
    }
}