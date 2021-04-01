using System;
using System.Collections.Generic;
using System.Text;

namespace MvcCore.Utilities.Exceptions
{
    public class ShopOnlineExceptions : Exception
    {
        public ShopOnlineExceptions()
        {

        }

        public ShopOnlineExceptions(string message) : base(message)
        {

        }

        public ShopOnlineExceptions(string message, Exception inner) : base(message, inner)
        {

        }
    }

}
