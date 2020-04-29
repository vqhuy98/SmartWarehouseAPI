using System;
using System.Collections.Generic;

namespace SWHRestApiCore.Models.DBModels
{
    public partial class Staff
    {
        public Staff()
        {
            CheckStore = new HashSet<CheckStore>();
            Transaction = new HashSet<Transaction>();
        }

        public long Id { get; set; }
        public string AuthToken { get; set; }
        public string Name { get; set; }
        public string Gmail { get; set; }
        public string PositionId { get; set; }
        public string StoreId { get; set; }
        public string StatusId { get; set; }
        public string PicUrl { get; set; }
        public string DeviceToken { get; set; }

        public virtual Position Position { get; set; }
        public virtual Status Status { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<CheckStore> CheckStore { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
