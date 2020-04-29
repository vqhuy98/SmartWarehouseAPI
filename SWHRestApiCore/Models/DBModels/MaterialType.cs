using System;
using System.Collections.Generic;

namespace SWHRestApiCore.Models.DBModels
{
    public partial class MaterialType
    {
        public MaterialType()
        {
            Material = new HashSet<Material>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Material> Material { get; set; }
    }
}
