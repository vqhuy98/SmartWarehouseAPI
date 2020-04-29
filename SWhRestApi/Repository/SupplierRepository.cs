using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class SupplierRepository
    {
        public SupplierRepository() { }

        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        public DetailDto getSupplier(String id)
        {
            supplier supplier = db.suppliers.Where(s => s.id == id).FirstOrDefault();
            DetailDto dto = new DetailDto(supplier.id, supplier.name);
            return dto;
        }
    }
}