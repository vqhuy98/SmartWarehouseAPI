using Microsoft.EntityFrameworkCore;
using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using SWHRestApiCore.Models.DBModels;
using SWHRestApiCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class StoreRepository
    {
        private SWHDatabaseContext db = new SWHDatabaseContext();
        StatusRepository statusRepository = new StatusRepository();
        public StoreRepository() { }

        public StoreDto getStoreDto(String id)
        {
            Store store = db.Store.Where(s => s.Id == id).FirstOrDefault();
            if (store == null)
            {
                return null;
            }
            StoreDto storeDto = new StoreDto(store.Id, store.Name, store.Location, statusRepository.getStatus(store.StatusId));
            return storeDto;
        }

        public DetailDto getSimpleStoreDto(String id)
        {
            DetailDto storeDto = null;
            try
            {
                Store store = db.Store.Where(s => s.Id == id).FirstOrDefault();
                storeDto = new DetailDto(store.Id, store.Name);
            }
            catch (Exception e)
            {
                return null; ;
            }
            return storeDto;
        }

        internal List<DetailDto> getSimpleStore()
        {
            List<Store> stores = db.Store.ToList();
            List<DetailDto> storeDtos = new List<DetailDto>();
            foreach (Store store in stores)
            {
                DetailDto detail = getSimpleStoreDto(store.Id);
                storeDtos.Add(detail);
            }
            return storeDtos;
        }

        internal List<StoreDto> getStoreDtos()
        {
            List<Store> stores = db.Store.ToList();
            List<StoreDto> storeDtos = new List<StoreDto>();
            foreach (Store store in stores)
            {
                StoreDto storeDto = getStoreDto(store.Id);
                storeDtos.Add(storeDto);
            }
            return storeDtos;
        }

        internal void updateStore(StoreRequestDto storeDto)
        {
            Store store = db.Store.FirstOrDefault(s => s.Id == storeDto.id);
            if (store != null)
            {

                store.Name = storeDto.name;
                store.Location = storeDto.location;
                store.StatusId = storeDto.statusId;
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
            }else
            throw new Exception("no store match");
        }
        public bool storeExists(string id)
        {
            return db.Store.Count(e => e.Id == id) > 0;
        }

        internal void addStore(StoreRequestDto storeDto)
        {
            Store store = new Store();
            store.Id = storeDto.id;
            store.Name = storeDto.name;
            store.Location = storeDto.location;
            store.StatusId = storeDto.statusId;
            db.Store.Add(store);

            db.SaveChanges();
        }

        internal StoreDto deleteStore(string id)
        {
            Store store = db.Store.Find(id);
            if (store != null)
            {
                StoreDto storeDto = getStoreDto(id);
                db.Store.Remove(store);
                db.SaveChanges();
                return storeDto;
            }
            return null;
        }
    }
}