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
    public class StoresController : ApiController
    {
        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        private StoreRepository storeRepository = new StoreRepository();

        // GET: api/stores
        public List<StoreDto> Getstores()
        {
            List<store> stores = db.stores.ToList();
            List<StoreDto> storeDtos = new List<StoreDto>();
            foreach (store store in stores)
            {
                StoreDto storeDto = storeRepository.getStoreDto(store.id);
                storeDtos.Add(storeDto);
            }
            return storeDtos;
        }

        // GET: api/stores/5
        [ResponseType(typeof(store))]
        public IHttpActionResult Getstore(string id)
        {
            store store = db.stores.Find(id);
            if (store == null)
            {
                return NotFound();
            }
            StoreDto storeDto = storeRepository.getStoreDto(store.id);
            return Ok(storeDto);
        }

        // PUT: api/stores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstore(string id, StoreDto storeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != storeDto.id)
            {
                return BadRequest();
            }
            store store = db.stores.Find(id);
            store.name = storeDto.name;
            store.location = storeDto.location;
            store.status_id = storeDto.status.id;
            db.Entry(store).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!storeExists(id))
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

        // POST: api/stores
        [ResponseType(typeof(StoreDto))]
        public IHttpActionResult Poststore(StoreDto storeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            store store = new store();
            store.id = storeDto.id;
            store.name = storeDto.name;
            store.location = storeDto.location;
            store.status_id = storeDto.status.id;
            db.stores.Add(store);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (storeExists(store.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = store.id }, store);
        }

        // DELETE: api/stores/5
        [ResponseType(typeof(StoreDto))]
        public IHttpActionResult Deletestore(string id)
        {
            store store = db.stores.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            db.stores.Remove(store);
            db.SaveChanges();
            StoreDto storeDto = storeRepository.getStoreDto(store.id);

            return Ok(storeDto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool storeExists(string id)
        {
            return db.stores.Count(e => e.id == id) > 0;
        }

    }
}