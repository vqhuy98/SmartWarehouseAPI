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
    public class StoreMaterialController : ApiController
    {
        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        private StoreMaterialRepository storeMaterialRepository = new StoreMaterialRepository();
        
        // GET: api/StoreMaterial/5
        [Route("api/StoreMaterial/{storeId}")]
        [HttpGet]
        [ResponseType(typeof(List<StoreMaterialDto>))]
        public IHttpActionResult Getstore_material(string storeId)
        {
            store_material storeMaterial = db.store_material.Where(s => s.store_id == storeId).FirstOrDefault();
            if (storeMaterial == null)
            {
                return NotFound();
            }
            List<StoreMaterialDto> storeMaterialDtos = storeMaterialRepository.getStoreMaterials(storeMaterial.store_id);

            return Ok(storeMaterialDtos);
        }

        // PUT: api/StoreMaterial/5
        [Route("api/StoreMaterial/{StoreId}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstore_material(string StoreId, StoreMaterialRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (StoreId != requestDto.storeId)
            {
                return BadRequest();
            }

            store_material storeMaterial = new store_material();
            storeMaterial.store_id = requestDto.storeId;
            storeMaterial.material_id = requestDto.materialId;
            storeMaterial.amount = requestDto.amount;

            db.Entry(storeMaterial).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!store_materialExists(requestDto))
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

        // POST: api/StoreMaterial
        [Route("api/StoreMaterial")]
        [HttpPost]
        [ResponseType(typeof(StoreMaterialDto))]
        public IHttpActionResult Poststore_material(StoreMaterialRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            store_material storeMaterial = new store_material();
            storeMaterial.store_id = requestDto.storeId;
            storeMaterial.material_id = requestDto.materialId;
            storeMaterial.amount = requestDto.amount;

            db.store_material.Add(storeMaterial);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (store_materialExists(requestDto))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            StoreMaterialDto storeMaterialDto = storeMaterialRepository.getStoreMaterial(requestDto);

            return Ok(storeMaterialDto);
        }

        // DELETE: api/StoreMaterial/5
        [ResponseType(typeof(store_material))]
        public IHttpActionResult Deletestore_material(string id)
        {
            store_material store_material = db.store_material.Find(id);
            if (store_material == null)
            {
                return NotFound();
            }

            db.store_material.Remove(store_material);
            db.SaveChanges();

            return Ok(store_material);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool store_materialExists(StoreMaterialRequestDto requestDto)
        {
            return db.store_material.Count(e => e.store_id == requestDto.storeId && 
            e.material_id == requestDto.materialId ) > 0;
        }
    }
}