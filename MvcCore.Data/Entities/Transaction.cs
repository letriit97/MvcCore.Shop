using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace MvcCore.Data.Entities
{
    public class Transaction
    {
        public int ID { set; get; }
        public DateTime TransactionDate { set; get; }
        public string ExternalTransactionId { set; get; }
        public decimal Amount { set; get; }
        public decimal Fee { set; get; }
        public string Result { set; get; }
        public string Message { set; get; }
        public TransactionStatus Status { set; get; }
        public string Provider { set; get; }

        public Guid UserId { get; set; }

        public AppUsers AppUser { get; set; }

    }
}
