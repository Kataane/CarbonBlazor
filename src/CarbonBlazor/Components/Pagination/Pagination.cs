using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CarbonBlazor
{
    public partial class Pagination
    {
        [Parameter]
        public EventCallback<int> PageChanged { get; set; }   

        [Parameter]
        public EventCallback<int> PageSizeChanged { get; set; }   

        [Parameter]
        public int Page { get; set; } = 1;

        [Parameter]
        public int PageSize { get; set; } = 5;

        [Parameter]
        public int[] PageSizes { get; set; } = new[] {5, 8, 10, 15, 20, 50};

        [Parameter]
        public int TotalItems { get; set; } = 1;

        private int startRange = 1;

        private int endRange = 0;

        private double totalPages;

        private const int pageSizeDefault = 5;

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            Update();
        }

        private void PrevPage()
        {
            if (Page == 1) return;

            Page--;

            UpdateRanges();
            PageChanged.InvokeAsync(Page);
        }

        private void NextPage()
        {
            if (Page == totalPages) return;

            Page++;

            UpdateRanges();
            PageChanged.InvokeAsync(Page);
        }

        private void SelectPageSize(ChangeEventArgs args)
        {
            if (int.TryParse(args.Value.ToString(), out var pageSize))
                PageSize = pageSize;
            else
                pageSize = pageSizeDefault;

            PageSizeChanged.InvokeAsync(PageSize);
            Reset();
        }

        private void SelectPage(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value.ToString(), out var page))
                Page = page;
            else
                Page = 1;

            PageChanged.InvokeAsync(Page);    
            UpdateRanges();
        }

        private void Reset()
        {
            Page = 1;

            Update();
            PageChanged.InvokeAsync(Page);
        }

        private void Update()
        {
            UpdateTotalPages();
            UpdateRanges();
        }

        private void UpdateRanges()
        {
            startRange = (Page - 1) * PageSize + 1;
            endRange = Math.Min(Page * PageSize, TotalItems);
        }

        private void UpdateTotalPages()
        {
            float value = (float)TotalItems / (float)PageSize;
            totalPages = Math.Ceiling(value);

            if (totalPages > 0) return;

            totalPages = 1;
        }
    }   
}