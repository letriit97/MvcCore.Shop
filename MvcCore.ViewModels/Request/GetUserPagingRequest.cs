using MvcCore.ViewModels.BaseCommons;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.ViewModels.Request
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string KeyWord { get; set; }
    }
}
