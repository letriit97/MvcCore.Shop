using MvcCore.ViewModels.BaseCommons;
using System.Collections.Generic;

namespace MvcCore.ViewModels.Request
{
    public class GetProductAdminPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public List<int> CategoryID { get; set; }

        public string LanguageId { get; set; }
    }
}