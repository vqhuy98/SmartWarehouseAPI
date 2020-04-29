using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Models.ViewModels
{
    public class StoreMaterialDto
    {
        public DetailDto simpleStore { get; set; }
        public MaterialDto simpleMaterial { get; set; }
        public long amount { get; set; }

        public string unit { get; set; }
        public string status { get; set; }

        public StoreMaterialDto(){  }

        public StoreMaterialDto(DetailDto simpleStore, MaterialDto simpleMaterial, long amount, string unit,string status)
        {
            this.simpleStore = simpleStore;
            this.simpleMaterial = simpleMaterial;
            this.amount = amount;
            this.unit = unit;
            this.status = status;
        }
    }
}