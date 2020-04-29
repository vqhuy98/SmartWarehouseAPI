using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using SWHRestApiCore.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class TypeRepository
    {
        public TypeRepository() { }

        private SWHDatabaseContext db = new SWHDatabaseContext();
        public DetailDto getMaterialType(String id)
        {
            MaterialType type = db.MaterialType.Where(s => s.Id == id).FirstOrDefault();
            DetailDto dto = new DetailDto(type.Id, type.Name);
            return dto;
        }

        public DetailDto getTransactionType(String id)
        {
            TransactionType transaction_Type = db.TransactionType.Where(s => s.Id == id).FirstOrDefault();
            DetailDto dto = new DetailDto(transaction_Type.Id, transaction_Type.Name);
            return dto;
        }

    }
}