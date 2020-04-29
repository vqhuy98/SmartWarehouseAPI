using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using SWHRestApiCore.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class StoreMaterialRepository
    {
        private SWHDatabaseContext db = new SWHDatabaseContext();
        private MaterialRepository materialRepository = new MaterialRepository();
        private StoreRepository storeRepository = new StoreRepository();

        public StoreMaterialRepository() { }

        public List<StoreMaterialDto> getStoreMaterials(string storeId)
        {
            List<StoreMaterialDto> result = new List<StoreMaterialDto>();
            List<StoreMaterial> storeMaterials = db.StoreMaterial.Where(s => s.StoreId == storeId && s.Amount> 0).ToList();
            foreach (StoreMaterial storeMaterial in storeMaterials)
            {
                Material m = db.Material.Where(s => s.Id == storeMaterial.MaterialId).FirstOrDefault();
                StoreMaterialDto storeMaterialDto = new StoreMaterialDto(storeRepository.getSimpleStoreDto(storeMaterial.StoreId),
                materialRepository.getMaterial(storeMaterial.MaterialId), storeMaterial.Amount,storeMaterial.Unit,db.Status.Where(s => s.Id == m.StatusId).FirstOrDefault().Name);
                result.Add(storeMaterialDto);
            }
            return result;
        }

        public StoreMaterialDto getStoreMaterial(StoreMaterialRequestDto dto)
        {
            StoreMaterial storeMaterial = db.StoreMaterial.Where(s => s.StoreId == dto.storeId &&
            s.MaterialId == dto.materialId && s.Amount > 0).FirstOrDefault();
            Material m = db.Material.Where(s => s.Id == storeMaterial.MaterialId).FirstOrDefault();
            StoreMaterialDto storeMaterialDto = new StoreMaterialDto(storeRepository.getSimpleStoreDto(storeMaterial.StoreId),
                materialRepository.getMaterial(storeMaterial.MaterialId), storeMaterial.Amount,storeMaterial.Unit, m.Status.Name);
            return storeMaterialDto;
        }

        internal void updateStoreMaterial(string storeId, StoreMaterialRequestDto requestDto)
        {
            StoreMaterial storeMaterial = new StoreMaterial();
            storeMaterial.StoreId = requestDto.storeId;
            storeMaterial.MaterialId = requestDto.materialId;
            storeMaterial.Amount = requestDto.amount;
            storeMaterial.Unit = requestDto.unit;

            db.Entry(storeMaterial).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();
        }

        internal void addStoreMaterial(StoreMaterialRequestDto requestDto)
        {

            StoreMaterial storeMaterial = new StoreMaterial();
            storeMaterial.StoreId = requestDto.storeId;
            storeMaterial.MaterialId = requestDto.materialId;
            storeMaterial.Amount = requestDto.amount;
            storeMaterial.Unit = requestDto.unit;

            db.StoreMaterial.Add(storeMaterial);
            db.SaveChanges();
        }
        public bool store_materialExists(StoreMaterialRequestDto requestDto)
        {
            return db.StoreMaterial.Count(e => e.StoreId == requestDto.storeId &&
            e.MaterialId == requestDto.materialId) > 0;
        }
    }
}