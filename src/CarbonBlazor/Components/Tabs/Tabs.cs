using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class Tabs : BaseComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Hidden { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public int Selected { get; set; } = -1;

        [Parameter]
        public EventCallback<int> SelectedChanged { get; set; }

        [Parameter]
        public bool Container { get; set; }

        private List<Tab> _tabs = new List<Tab>();

        private RenderFragment _selected;

        protected override void SetClass()
        {
            _mapper
                .Clear()
                .SetBase("cb--tabs")
                .Add("cb--tabs--container", Container)
                .Add("cb--tabs--light", Light)
                .Add(Class);
        }

        public void AddTab(Tab tab)
        {
            _tabs.Add(tab);

            //? Premature optimization. 
            //? If alredy tab witch content selected skip all this part
            if (!(_selected is null)) return;

            if (Selected == -1)
            {
                Select(_tabs[0].Id);
                return;
            }

            if (Selected != -1 && _tabs.Count == Selected)
            {
                Select(tab.Id);
                return;
            }

            //TODO: When Selected out of range. Set Selected equals first element
        }

        public void Select(string id)
        {
            if (!_tabs.Exists(s => string.Equals(s.Id, id))) return;

            var st = _tabs.FirstOrDefault(s => s.Selected == true);
            if (!(st is null)) st.Selected = false;

            var tab = _tabs.FirstOrDefault(s => string.Equals(s.Id, id));
            tab.Selected = true;
            _selected = tab.ChildContent;
            Selected = _tabs.IndexOf(tab);

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