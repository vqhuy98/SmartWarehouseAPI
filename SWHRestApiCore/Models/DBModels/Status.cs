using System;
using System.Collections.Generic;

namespace SWHRestApiCore.Models.DBModels
{
    public partial class Status
    {
        public Status()
        {
            Material = new HashSet<Material>();
            Staff = new HashSet<Staff>();
            Store = new HashSet<Store>();
            Transaction = new HashSet<Transaction>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Material> Material { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
        public virtual ICollection<Store> Store { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
