using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class PositionRepository
    {
        private SWHDatabaseEntities db = new SWHDatabaseEntities();

        public PositionRepository() { }

        public DetailDto getPosision(String id)
        {
            position position = db.positions.Where(s => s.id == id).FirstOrDefault();
            DetailDto positionDto = new DetailDto(position.id, position.name);
            return positionDto;
        } 
    }
}