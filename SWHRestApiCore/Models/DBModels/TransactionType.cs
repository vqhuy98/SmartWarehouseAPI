using System;
using System.Collections.Generic;

namespace SWHRestApiCore.Models.DBModels
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            Transaction = new HashSet<Transaction>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
