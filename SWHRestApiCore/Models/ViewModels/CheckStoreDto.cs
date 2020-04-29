using SWhRestApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRestApiCore.Models.ViewModels
{
    public class CheckStoreDto
    {
        public int id { get; set; }
        public DetailDto simpleStore { get; set; }
        public DetailDto simpleStaff { get; set; }
        public DateTime Time { get; set; }

        public List<TransactionMaterialAmountDto> detail { get; set; }

        public CheckStoreDto(int id, DetailDto simpleStore, DetailDto simpleStaff, DateTime time, List<TransactionMaterialAmountDto> detail)
        {
            this.id = id;
            this.simpleStore = simpleStore;
            this.simpleStaff = simpleStaff;
            Time = time;
            this.detail = detail;
        }

        public CheckStoreDto()
        {
        }
    }
}
