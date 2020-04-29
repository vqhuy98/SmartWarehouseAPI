using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using SWHRestApiCore.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class StatusRepository
    {
        private SWHDatabaseContext db = new SWHDatabaseContext();
        public StatusRepository()
        {
        }

        public DetailDto getStatus(String id)
        {
            Status status = db.Status.Where(s => s.Id == id).FirstOrDefault();
            DetailDto dto = new DetailDto(status.Id, status.Name);
            return dto;
        }



    }
}