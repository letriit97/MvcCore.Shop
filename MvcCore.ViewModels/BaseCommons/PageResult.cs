using System.Collections.Generic;

namespace MvcCore.ViewModels.BaseCommons
{
    public class PageResult<T> : PaginationBase
    {
        public List<T> Items { get; set; }
    }
}