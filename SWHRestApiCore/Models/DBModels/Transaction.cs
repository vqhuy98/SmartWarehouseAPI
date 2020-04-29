using System;
using System.Collections.Generic;

namespace SWHRestApiCore.Models.DBModels
{
    public partial class Transaction
    {
        public Transaction()
        {
            TransactionDetail = new HashSet<TransactionDetail>();
        }

        public int Id { get; set; }
        public string ExchangeStoreId { get; set; }
        public string StoreId { get; set; }
        public DateTime Time { get; set; }
        public long StaffId { get; set; }
        public string TransactionTypeId { get; set; }
        public string StatusId { get; set; }

        public virtual Store ExchangeStore { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Status Status { get; set; }
        public virtual Store Store { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetail { get; set; }
    }
}
