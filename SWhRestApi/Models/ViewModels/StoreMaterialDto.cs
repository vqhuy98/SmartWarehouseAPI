using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Models.ViewModels
{
    public class StoreMaterialDto
    {
        public DetailDto simpleStore { get; set; }
        public DetailDto simpleMaterial { get; set; }
        public long amount { get; set; }

        public StoreMaterialDto()
        {
        }

        public StoreMaterialDto(DetailDto simpleStore, DetailDto simpleMaterial, long amount)
        {
            this.simpleStore = simpleStore;
            this.simpleMaterial = simpleMaterial;
            this.amount = amount;
        }
    }
}