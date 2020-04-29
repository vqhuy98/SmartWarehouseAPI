using SWhRestApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRestApiCore.Models.ViewModels
{
    public class CheckStoreRequestDto
    {
        public int id { get; set; }
        public string storeId { get; set; }
        public string staffId { get; set; }
        public long time { get; set; }

        public List<TransactionMaterialAmountDto> detail { get; set; }

        public CheckStoreRequestDto()
        {
        }

        public CheckStoreRequestDto(int id, string storeId, string staffId, long time, List<TransactionMaterialAmountDto> detail)
        {
            this.id = id;
            this.storeId = storeId;
            this.staffId = staffId;
            this.time = time;
            this.detail = detail;
        }
    }
}
