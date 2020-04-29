using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWhRestApi.Models.ViewModels;
using SWHRestApiCore.Models.DBModels;

namespace SWHRestApiCore.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "NV,QL")]
    [Route("api/device")]
    [ApiController]
    public class DeviceTokenController : ControllerBase
    {
        private SWHDatabaseContext db = new SWHDatabaseContext();

        [HttpPost("login")]
        public dynamic AddDeviceToken([FromBody] DetailDto dto)
        {
            Staff staff = db.Staff.FirstOrDefault(s => s.Id == long.Parse(dto.id));
            if(staff == null)
            {
                return false;
            }
            staff.DeviceToken = dto.name;
            db.Entry(staff).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        [HttpPost("logout")]
        public dynamic RemoveDeviceToken([FromBody] DetailDto dto)
        {
            Staff staff = db.Staff.FirstOrDefault(s => s.Id == long.Parse(dto.id));
            if (staff == null)
            {
                return false;
            }
            staff.DeviceToken = null;
            db.Entry(staff).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return true;
        }

    }
}