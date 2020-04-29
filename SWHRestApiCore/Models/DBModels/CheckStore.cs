using System;
using System.Collections.Generic;

namespace SWHRestApiCore.Models.DBModels
{
    public partial class CheckStore
    {
        public CheckStore()
        {
            CheckStoreDetail = new HashSet<CheckStoreDetail>();
        }

        public int Id { get; set; }
        public string StoreId { get; set; }
        public long StaffId { get; set; }
        public DateTime Time { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<CheckStoreDetail> CheckStoreDetail { get; set; }
    }
}
