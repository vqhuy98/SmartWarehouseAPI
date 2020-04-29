using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Models.ViewModels
{
    public class TransactionRequestDto
    {

        public int id { get; set; }
        public string exchangeStoreId { get; set; }
        public string storeId { get; set; }
        public double time { get; set; }
        public int staffId { get; set; }
        public string transactionTypeId { get; set; }
        public string statusId { get; set; }
        public List<TransactionMaterialAmountDto> detail { get; set; }

        public TransactionRequestDto(int id, string exchangeStoreId, string storeId,
            double time, int staffId, string transactionTypeId, string statusId, List<TransactionMaterialAmountDto> detail)
        {
            this.id = id;
            this.exchangeStoreId = exchangeStoreId;
            this.storeId = storeId;
            this.time = time;
            this.staffId = staffId;
            this.transactionTypeId = transactionTypeId;
            this.statusId = statusId;
            this.detail = detail;
        }

        public TransactionRequestDto()
        {
        }
    }
}