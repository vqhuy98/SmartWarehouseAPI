using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using SWhRestApi.Repository;

namespace SWhRestApi.Controllers
{
    [Route("api/staff")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private StaffRepository staffRepository = new StaffRepository();

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "QL")]
        [HttpGet]
        public List<StaffDto> Getstaffs()
        {
            List<StaffDto> staffs = staffRepository.getStaffs();

            return staffs;
        }

        [HttpGet]
        [Route("{id}")]
        public dynamic GetstaffByAccessToken(long id)
        {
            StaffDto dto = staffRepository.getStaff(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpPut]
        [Route("{id}")]
        public dynamic Putstaff(long id, [FromBody] StaffRequestDto staffDto)
        {
            String authen_token = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != staffDto.id)
            {
                return BadRequest();
            }

            try
            {
                authen_token = staffRepository.updateStaff(id, staffDto);
            }
            catch (Exception e)
            {
                if (!staffRepository.staffExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw e;
                }
            }

            return Ok(authen_token);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "QL")]
        [HttpPost]
        public dynamic Poststaff([FromBody] StaffRequestDto staffDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                staffDto = staffRepository.addStaff(staffDto);
            }
            catch (Exception e)
            {
                if (staffRepository.staffExists(staffDto.id))
                {
                    return Conflict();
                }
                else
                {
                    throw e;
                }
            }

            return Ok(staffDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "QL")]
        [HttpDelete("{id}")]
        public dynamic Deletestaff(long id)
        {
            StaffDto dto = staffRepository.deleteStaff(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }


    }
}