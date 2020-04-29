using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using SWHRestApiCore.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class PositionRepository
    {
        private SWHDatabaseContext db = new SWHDatabaseContext();

        public PositionRepository() { }

        public DetailDto getPosision(String id)
        {
            Position position = db.Position.Where(s => s.Id == id).FirstOrDefault();
            DetailDto positionDto = new DetailDto(position.Id, position.Name);
            return positionDto;
        }
    }
}