using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Models.ViewModels
{
    public class StoreMaterialRequestDto
    {
        public string storeId { get; set; }
        public int materialId { get; set; }
        public long amount { get; set; }
        public String unit { get; set; }

        public StoreMaterialRequestDto()
        {
        }

        public StoreMaterialRequestDto(string storeId, int materialId, long amount, string unit)
        {
            this.storeId = storeId;
            this.materialId = materialId;
            this.amount = amount;
            this.unit = unit;
        }
    }
}