using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class StatusRepository
    {
        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        public StatusRepository()
        {
        }

        public DetailDto getStatus(String id)
        {
            status status = db.status.Where(s => s.id == id).FirstOrDefault();
            DetailDto dto = new DetailDto(status.id, status.name);
            return dto;
        } 


    
    }
}