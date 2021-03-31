using MvcCore.ViewModels.BaseCommons;
using System.Collections.Generic;

namespace MvcCore.ViewModels.Request
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryID { get; set; }
    }
}