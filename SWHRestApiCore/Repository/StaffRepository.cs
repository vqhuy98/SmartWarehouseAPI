using Microsoft.IdentityModel.Tokens;
using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using SWHRestApiCore.Models.DBModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace SWhRestApi.Repository
{
    public class StaffRepository
    {

        private SWHDatabaseContext db = new SWHDatabaseContext();
        StoreRepository storeRepository = new StoreRepository();
        StatusRepository statusRepository = new StatusRepository();
        private static string SECRETKEY = "SWHDatabaseSE1268";


        PositionRepository positionRepository = new PositionRepository();

        public StaffRepository() { }

        public StaffDto getStaff(long id)
        {
            Staff staff = db.Staff.Where(s => s.Id == id).FirstOrDefault();
            StaffDto dto = new StaffDto(staff.Id, staff.Name, staff.Gmail,
                    positionRepository.getPosision(staff.PositionId),
                    storeRepository.getStoreDto(staff.StoreId),
                    statusRepository.getStatus(staff.StatusId), staff.PicUrl, staff.AuthToken,staff.DeviceToken);
            return dto;
        }

        internal List<StaffDto> getStaffs()
        {
            List<Staff> staffs = db.Staff.ToList();
            List<StaffDto> results = new List<StaffDto>();
            foreach (Staff staff in staffs)
            {
                StaffDto dto = getStaff(staff.Id);

                results.Add(dto);
            }
            return results;
        }

        public DetailDto getSimpleStaff(long id)
        {

            Staff staff = db.Staff.Where(s => s.Id == id).FirstOrDefault();
            DetailDto dto = new DetailDto(staff.Id.ToString(), staff.Name);
            return dto;
        }

        internal String updateStaff(long id, StaffRequestDto staffDto)
        {
            String tokenString = null;
            Staff staff = db.Staff.Where(s => s.Id == id).FirstOrDefault();
            if (staff != null)
            {
                staff.Name = staffDto.name;
                staff.Gmail = staffDto.gmail;
                staff.PositionId = staffDto.positionId;
                staff.StoreId = staffDto.storeId;
                staff.StatusId = staffDto.statusId;
                staff.PicUrl = staffDto.picUrl;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.Default.GetBytes(SECRETKEY);
                var claim = new Claim(ClaimTypes.NameIdentifier, staff.Id.ToString());
                var claim2 = new Claim(ClaimTypes.Name, staff.Name);
                var claim3 = new Claim(ClaimTypes.Role, staff.PositionId);
                List<Claim> claims = new List<Claim>() { claim, claim2, claim3 };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "SWH",
                    Audience = "SWH",
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                tokenString = tokenHandler.WriteToken(token);

                staff.AuthToken = tokenString;


            }

            db.Entry(staff).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return tokenString;
        }

        internal StaffRequestDto addStaff(StaffRequestDto staffDto)
        {

            Staff staff = new Staff();
            staff.Id = staffDto.id;
            staff.Name = staffDto.name;
            staff.Gmail = staffDto.gmail;
            staff.PositionId = staffDto.positionId;
            staff.StoreId = staffDto.storeId;
            staff.StatusId = staffDto.statusId;
            staff.PicUrl = staffDto.picUrl;

            // set username password trong db
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.Default.GetBytes(SECRETKEY);
            var claim = new Claim(ClaimTypes.NameIdentifier, staff.Id.ToString());
            var claim2 = new Claim(ClaimTypes.Name, staff.Name);
            var claim3 = new Claim(ClaimTypes.Role, staff.PositionId);
            List<Claim> claims = new List<Claim>() { claim, claim2, claim3 };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "SWH",
                Audience = "SWH",
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            staff.AuthToken = tokenString;
            staffDto.authToken = tokenString;
            db.Staff.Add(staff);
            db.SaveChanges();
            return staffDto;

        }

        internal StaffDto deleteStaff(long id)
        {
            Staff staff = db.Staff.Where(s => s.Id == id).FirstOrDefault();
            StaffDto staffDto = new StaffDto();
            if (staff != null)
            {
                staffDto = getStaff(id);
                db.Staff.Remove(staff);
                db.SaveChanges();

            }
            return staffDto;
        }

        public DetailDto getSimpleStaff(int id)
        {

            Staff staff = db.Staff.Where(s => s.Id == id).FirstOrDefault();
            DetailDto dto = new DetailDto(staff.Id.ToString(), staff.Name);
            return dto;
        }
        public bool staffExists(long id)
        {
            return db.Staff.Count(e => e.Id == id) > 0;
        }
    }
}