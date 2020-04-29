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
    public class MaterialsController : ApiController
    {
        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        private MaterialRepository materialRepository = new MaterialRepository();

        // GET: api/materials
        public List<MaterialDto> Getmaterials()
        {
            List<material> materials = db.materials.ToList();
            List<MaterialDto> materialDtos = new List<MaterialDto>();
            foreach (material m in materials)
            {
                MaterialDto materialDto = materialRepository.getMaterial(m.id);
                materialDtos.Add(materialDto);
            }
            return materialDtos;
        }
        // GET: api/materials/5
        [ResponseType(typeof(MaterialDto))]
        public IHttpActionResult Getmaterial(int id)
        {
            material material = db.materials.Find(id);
            if (material == null)
            {
                return NotFound();
            }
            MaterialDto materialDto = materialRepository.getMaterial(material.id);
            return Ok(materialDto);
        }

        // PUT: api/materials/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmaterial(int id, MaterialDto materialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialDto.id)
            {
                return BadRequest();
            }

            material material = db.materials.Find(id);
            material.name = materialDto.name;
            material.supplier_infor_id = materialDto.supplier.id;
            material.barcode = materialDto.barcode;
            material.type_id = materialDto.type.id;
            material.main_unit = materialDto.mainUnit;
            material.change_unit = materialDto.changeUnit;
            material.status_id = materialDto.status.id;

            db.Entry(material).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!materialExists(id))
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

        // POST: api/materials
        [ResponseType(typeof(MaterialDto))]
        public IHttpActionResult Postmaterial(MaterialDto materialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            material material = new material();
            material.id = materialDto.id;
            material.name = materialDto.name;
            material.supplier_infor_id = materialDto.supplier.id;
            material.barcode = materialDto.barcode;
            material.type_id = materialDto.type.id;
            material.main_unit = materialDto.mainUnit;
            material.change_unit = materialDto.changeUnit;
            material.status_id = materialDto.status.id;

            db.materials.Add(material);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (materialExists(material.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = material.id }, material);
        }

        // DELETE: api/materials/5
        [ResponseType(typeof(MaterialDto))]
        public IHttpActionResult Deletematerial(int id)
        {
            material material = db.materials.Find(id);
            if (material == null)
            {
                return NotFound();
            }

            db.materials.Remove(material);
            db.SaveChanges();
            MaterialDto materialDto = materialRepository.getMaterial(material.id);

            return Ok(materialDto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool materialExists(int id)
        {
            return db.materials.Count(e => e.id == id) > 0;
        }
    }
}