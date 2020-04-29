using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Models.ViewModels
{
    public class TransactionMaterialAmountDto
    {
        public DetailDto material { get; set; }
        public long materialAmount { get; set; }
        public string unit { get; set; }

        public TransactionMaterialAmountDto()
        {
        }

        public TransactionMaterialAmountDto(DetailDto material, long materialAmount, string unit)
        {
            this.material = material;
            this.materialAmount = materialAmount;
            this.unit = unit;
        }
    }
}