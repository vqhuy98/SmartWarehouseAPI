using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class StaffRepository
    {
        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        StoreRepository storeRepository = new StoreRepository();
        StatusRepository statusRepository = new StatusRepository();
        PositionRepository positionRepository = new PositionRepository();

        public StaffRepository() { }

        public StaffDto getStaff(string accessToken)
        {
            staff staff = db.staffs.Where(s => s.auth_token == accessToken).FirstOrDefault();
            StaffDto dto = new StaffDto(staff.id.ToString(), staff.name, staff.gmail,
                    positionRepository.getPosision(staff.position_id),
                    storeRepository.getStoreDto(staff.store_id),
                    statusRepository.getStatus(staff.status_id), staff.pic_url, staff.auth_token);
            return dto;
        }

        public DetailDto getSimpleStaff(String accessToken)
        {

            staff staff = db.staffs.Where(s => s.auth_token == accessToken).FirstOrDefault();
            DetailDto dto = new DetailDto(staff.id.ToString(), staff.name);
            return dto;
        }

        public DetailDto getSimpleStaff(int id)
        {

            staff staff = db.staffs.Where(s => s.id == id).FirstOrDefault();
            DetailDto dto = new DetailDto(staff.id.ToString(), staff.name);
            return dto;
        }
    }
}