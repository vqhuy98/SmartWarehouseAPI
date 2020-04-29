using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class StoreRepository
    {
        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        StatusRepository statusRepository = new StatusRepository();
        public StoreRepository() { }

        public StoreDto getStoreDto(String id)
        {
            store store = db.stores.Where(s => s.id == id).FirstOrDefault();
            StoreDto storeDto = new StoreDto(store.id, store.name, store.location, statusRepository.getStatus(store.status_id));
            return storeDto;
        }

        public DetailDto getSimpleStoreDto(String id)
        {
            DetailDto storeDto = null;
            try
            {
                store store = db.stores.Where(s => s.id == id).FirstOrDefault();
                storeDto = new DetailDto(store.id, store.name);
            }
            catch (Exception e)
            {
            }
            return storeDto;
        }
    }
}