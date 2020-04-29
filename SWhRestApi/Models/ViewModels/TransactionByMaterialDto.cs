using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Models.ViewModels
{
    public class TransactionByMaterialDto
    {
        public DateTime time { get; set; }
        public DetailDto exahangeStore { get; set; }
        public DetailDto staff { get; set; }
        public DetailDto transactionType { get; set; }
        public DetailDto status { get; set; }
        public DetailDto material { get; set; }
        public long materialAmount { get; set; }

        public TransactionByMaterialDto()
        {
        }

        public TransactionByMaterialDto(DateTime time,
            DetailDto exahangeStore, DetailDto staff, DetailDto transactionType, DetailDto status, DetailDto material, long materialAmount)
        {
            this.time = time;
            this.exahangeStore = exahangeStore;
            this.staff = staff;
            this.transactionType = transactionType;
            this.status = status;
            this.material = material;
            this.materialAmount = materialAmount;
        }
    }
}