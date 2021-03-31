using System;

namespace MvcCore.ViewModels.BaseCommons
{
    public class PaginationBase
    {
        // Trang hiện tại
        public int PageIndex { get; set; }

        //Số item/1 trang
        public int PageSize { get; set; }

        // Tổng số items
        public int TotalItems { get; set; }

        //Tổng số trang
        public int Paging
        {
            get
            {
                var countItem = (double)TotalItems / PageSize;
                return (int)Math.Ceiling(countItem);
            }
        }
    }
}