using System;
using System.Collections.Generic;

namespace SWHRestApiCore.Models.DBModels
{
    public partial class Position
    {
        public Position()
        {
            Staff = new HashSet<Staff>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Staff> Staff { get; set; }
    }
}
