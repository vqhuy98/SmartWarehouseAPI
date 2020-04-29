using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRestApiCore.Models.ViewModels
{
    public class MaterialRequestDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string supplierId { get; set; }
        public int barcode { get; set; }
        public string typeId { get; set; }
        public string changeUnit { get; set; }
        public string statusId { get; set; }
        public string exp { get; set; }

        public MaterialRequestDto(int id, string name, string supplierId, int barcode, string typeId, string changeUnit, string statusId,string exp)
        {
            this.id = id;
            this.name = name;
            this.supplierId = supplierId;
            this.barcode = barcode;
            this.typeId = typeId;
            this.changeUnit = changeUnit;
            this.statusId = statusId;
            this.exp = exp;
        }

        public MaterialRequestDto()
        {
        }
    }
}
