using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Models.ViewModels
{
    public class MaterialDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public DetailDto supplier { get; set; }
        public int barcode { get; set; }
        public DetailDto type { get; set; }
        public string changeUnit { get; set; }
        public DetailDto status { get; set; }
        public string exp { get; set; }

        public MaterialDto()
        {
        }

        public MaterialDto(int id, string name, DetailDto supplier, int barcode, DetailDto type, string changeUnit, DetailDto status,string exp)
        {
            this.id = id;
            this.name = name;
            this.supplier = supplier;
            this.barcode = barcode;
            this.type = type;
            this.changeUnit = changeUnit;
            this.status = status;
            this.exp = exp;
        }
    }
}