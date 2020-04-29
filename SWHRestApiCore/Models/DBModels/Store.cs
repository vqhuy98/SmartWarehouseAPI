using System;
using System.Collections.Generic;

namespace SWHRestApiCore.Models.DBModels
{
    public partial class Store
    {
        public Store()
        {
            CheckStore = new HashSet<CheckStore>();
            Staff = new HashSet<Staff>();
            StoreMaterial = new HashSet<StoreMaterial>();
            TransactionExchangeStore = new HashSet<Transaction>();
            TransactionStore = new HashSet<Transaction>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string StatusId { get; set; }

        public virtual Status Status { get; set; }
        public virtual ICollection<CheckStore> CheckStore { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
        public virtual ICollection<StoreMaterial> StoreMaterial { get; set; }
        public virtual ICollection<Transaction> TransactionExchangeStore { get; set; }
        public virtual ICollection<Transaction> TransactionStore { get; set; }
    }
}
