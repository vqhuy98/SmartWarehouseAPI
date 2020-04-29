using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using SWHRestApiCore.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class SupplierRepository
    {
        public SupplierRepository() { }

        private SWHDatabaseContext db = new SWHDatabaseContext();
        public DetailDto getSupplier(String id)
        {
            Supplier supplier = db.Supplier.Where(s => s.Id == id).FirstOrDefault();
            DetailDto dto = new DetailDto(supplier.Id, supplier.Name);
            return dto;
        }

        internal List<DetailDto> getSuplliers()
        {
            List<DetailDto> detailDtos = new List<DetailDto>();
            List<Supplier> suppliers = db.Supplier.ToList();
            foreach(Supplier supplier in suppliers)
            {
                DetailDto dto = new DetailDto(supplier.Id, supplier.Name);
                detailDtos.Add(dto);
            }
            return detailDtos;
        }
    }
}