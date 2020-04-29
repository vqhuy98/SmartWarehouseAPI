using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class StoreMaterialRepository
    {
        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        private MaterialRepository materialRepository = new MaterialRepository();
        private StoreRepository storeRepository = new StoreRepository();

        public StoreMaterialRepository() { }

        public List<StoreMaterialDto> getStoreMaterials(string storeId)
        {
            List<StoreMaterialDto> result = new List<StoreMaterialDto>();
            List<store_material> storeMaterials = db.store_material.Where(s => s.store_id == storeId).ToList();
            foreach(store_material storeMaterial in storeMaterials){
                StoreMaterialDto storeMaterialDto = new StoreMaterialDto(storeRepository.getSimpleStoreDto(storeMaterial.store_id),
                materialRepository.getSimpleMaterial(storeMaterial.material_id), storeMaterial.amount);
                result.Add(storeMaterialDto);
            }
            return result;
        }

        public StoreMaterialDto getStoreMaterial(StoreMaterialRequestDto dto)
        {
            store_material storeMaterial = db.store_material.Where(s => s.store_id == dto.storeId &&
            s.material_id == dto.materialId ).FirstOrDefault();
            StoreMaterialDto storeMaterialDto = new StoreMaterialDto(storeRepository.getSimpleStoreDto(storeMaterial.store_id),
                materialRepository.getSimpleMaterial(storeMaterial.material_id), storeMaterial.amount);
            return storeMaterialDto;
        }
    }
}