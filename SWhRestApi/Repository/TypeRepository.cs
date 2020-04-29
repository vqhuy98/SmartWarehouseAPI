using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class TypeRepository
    {
        public TypeRepository() { }

        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        public DetailDto getMaterialType(String id)
        {
            material_type type = db.material_type.Where(s => s.id == id).FirstOrDefault();
            DetailDto dto = new DetailDto(type.id, type.name);
            return dto;
        }

        public DetailDto getTransactionType(String id)
        {
            transaction_type transaction_Type = db.transaction_type.Where(s => s.id == id).FirstOrDefault();
            DetailDto dto = new DetailDto(transaction_Type.id, transaction_Type.name);
            return dto;
        }

    }
}