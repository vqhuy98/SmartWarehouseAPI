using System;
using System.Collections.Generic;

namespace SWHRestApiCore.Models.DBModels
{
    public partial class CheckStoreDetail
    {
        public int CheckStoreInforId { get; set; }
        public int MaterialId { get; set; }
        public long? Amount { get; set; }

        public virtual CheckStore CheckStoreInfor { get; set; }
        public virtual Material Material { get; set; }
    }
}
