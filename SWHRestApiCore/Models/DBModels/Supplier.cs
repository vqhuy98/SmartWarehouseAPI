using System;
using System.Collections.Generic;

namespace SWHRestApiCore.Models.DBModels
{
    public partial class Supplier
    {
        public Supplier()
        {
            Material = new HashSet<Material>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Material> Material { get; set; }
    }
}
