using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRestApiCore.Models.ViewModels
{
    public class SupplierDto
    {
        public string id { get; set; }
        public string name { get; set; }

        public SupplierDto()
        {
        }

        public SupplierDto(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
