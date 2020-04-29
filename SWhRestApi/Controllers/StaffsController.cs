using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using SWhRestApi.Repository;

namespace SWhRestApi.Controllers
{
    public class StaffsController : ApiController
    {
        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        private StaffRepository staffRepository = new StaffRepository();
        

        // GET: api/Staffs
        public List<StaffDto> Getstaffs()
        {

            List<staff> staffs = db.staffs.ToList();
            List<StaffDto> results = new List<StaffDto>();
            foreach (staff staff in staffs)
            {
                StaffDto dto = staffRepository.getStaff(staff.auth_token);

                results.Add(dto);
            }

            return results;
        }

        // GET: api/Staffs/5
        [HttpGet]
        [Route("api/Staffs/{accessToken}")]
        [ResponseType(typeof(StaffDto))]
        public IHttpActionResult GetstaffByAccessToken(string accessToken)
        {
            staff staff = db.staffs.Where(s => s.auth_token == accessToken).FirstOrDefault();
            if (staff == null)
            {
                return NotFound();
            }
            StaffDto dto = staffRepository.getStaff(staff.auth_token);

            return Ok(dto);
        }

        // PUT: api/Staffs/5
        [Route("api/Staffs/{accessToken}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstaff(string accessToken, [FromBody] StaffRequestDto staffDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (accessToken != staffDto.accessToken)
            {
                return BadRequest();
            }

            staff staff = db.staffs.Where(s => s.auth_token == accessToken).FirstOrDefault();
            staff.name = staffDto.name;
            staff.gmail = staffDto.gmail;
            staff.position_id = staffDto.positionId;
            staff.store_id = staffDto.storeId;
            staff.status_id = staffDto.statusId;
            staff.pic_url = staffDto.picUrl;


            db.Entry(staff).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!staffExists(accessToken))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Staffs
        [ResponseType(typeof(StaffDto))]
        public IHttpActionResult Poststaff(StaffRequestDto staffDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            staff staff = new staff();
            staff.name = staffDto.name;
            staff.gmail = staffDto.gmail;
            staff.position_id = staffDto.positionId;
            staff.store_id = staffDto.storeId;
            staff.status_id = staffDto.statusId;
            staff.pic_url = staffDto.picUrl;
            staff.auth_token = staffDto.accessToken;

            db.staffs.Add(staff);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (staffExists(staffDto.accessToken))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(staffDto);
        }

        // DELETE: api/Staffs/5
        [Route("api/Staffs/{accessToken}")]
        [ResponseType(typeof(StaffDto))]
        public IHttpActionResult Deletestaff(string accessToken)
        {
            staff staff = db.staffs.Where(s => s.auth_token == accessToken).FirstOrDefault();
            if (staff == null)
            {
                return NotFound();
            }
            StaffDto dto = staffRepository.getStaff(staff.auth_token);

            db.staffs.Remove(staff);
            db.SaveChanges();
            
            return Ok(dto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool staffExists(String accesssToken)
        {
            return db.staffs.Count(e => e.auth_token == accesssToken) > 0;
        }
        
    }
}