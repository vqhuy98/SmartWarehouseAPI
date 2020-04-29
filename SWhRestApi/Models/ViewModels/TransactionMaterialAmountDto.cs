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

        public TransactionMaterialAmountDto()
        {
        }

        public TransactionMaterialAmountDto(DetailDto materialId, long materialAmount)
        {
            this.material = materialId;
            this.materialAmount = materialAmount;
        }
    }
}