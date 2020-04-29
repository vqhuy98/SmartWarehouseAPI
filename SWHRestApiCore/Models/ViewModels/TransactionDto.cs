using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Models.ViewModels
{
    public class TransactionDto
    {
        public int id { get; set; }
        public DetailDto exchangeStore { get; set; }
        public DetailDto store { get; set; }
        public System.DateTime time { get; set; }
        public DetailDto staff { get; set; }
        public DetailDto transactionType { get; set; }
        public DetailDto status { get; set; }
        public List<TransactionMaterialAmountDto> detail { get; set; }

        public TransactionDto()
        {
        }

        public TransactionDto(int id, DetailDto exchangeStore, DetailDto store,
            DateTime time, DetailDto staff, DetailDto transactionType, DetailDto status, List<TransactionMaterialAmountDto> detail)
        {
            this.id = id;
            this.exchangeStore = exchangeStore;
            this.store = store;
            this.time = time;
            this.staff = staff;
            this.transactionType = transactionType;
            this.status = status;
            this.detail = detail;
        }
    }
}