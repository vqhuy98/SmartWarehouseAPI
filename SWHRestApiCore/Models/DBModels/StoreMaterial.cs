using System;
using System.Collections.Generic;

namespace SWHRestApiCore.Models.DBModels
{
    public partial class StoreMaterial
    {
        public string StoreId { get; set; }
        public int MaterialId { get; set; }
        public long Amount { get; set; }
        public string Unit { get; set; }

        public virtual Material Material { get; set; }
        public virtual Store Store { get; set; }
    }
}
