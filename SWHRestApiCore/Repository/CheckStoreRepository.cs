using SWhRestApi.Models.ViewModels;
using SWhRestApi.Repository;
using SWHRestApiCore.Models.DBModels;
using SWHRestApiCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRestApiCore.Repository
{
    public class CheckStoreRepository
    {
        private SWHDatabaseContext db = new SWHDatabaseContext();
        private MaterialRepository materialRepository = new MaterialRepository();
        private StoreRepository storeRepository = new StoreRepository();
        private StaffRepository staffRepository = new StaffRepository();
        private TypeRepository typeRepository = new TypeRepository();
        private StatusRepository statusRepository = new StatusRepository();
        private TimeStampConvert stampConvert = new TimeStampConvert();

        public List<CheckStoreDto> getCheckStoreDtos(string storeId, double startDate, double endDate, int id, int limit)
        {
            List<CheckStoreDto> result = new List<CheckStoreDto>();
            /*List<CheckStore> checkStores = new List<CheckStore>();
            if (startDate == 0 && endDate == 0 && id == 0)
            {
                checkStores = db.CheckStore.Where(s => s.StoreId == storeId).ToList();
            }
            else
                checkStores = db.CheckStore.Where(s => s.StoreId == storeId &&
                stampConvert.convertTimeStamp(startDate) <= s.Time && s.Time <= stampConvert.convertTimeStamp(endDate)).ToList();
            if (id == 0)
            {
                int count = 0;
                foreach (CheckStore checkStore in checkStores)
                {
                    List<CheckStoreDetail> details = db.CheckStoreDetail.Where(s => s.CheckStoreInforId == checkStore.Id).ToList();
                    List<TransactionMaterialAmountDto> detailDtos = new List<TransactionMaterialAmountDto>();
                    foreach (CheckStoreDetail detail in details)
                    {
                        TransactionMaterialAmountDto detailDto = new TransactionMaterialAmountDto(
                            materialRepository.getSimpleMaterial(detail.MaterialId), detail.Amount.Value,detail);
                        detailDtos.Add(detailDto);
                    }
                    CheckStoreDto dto = new CheckStoreDto(checkStore.Id, storeRepository.getSimpleStoreDto(checkStore.StoreId),
                        staffRepository.getSimpleStaff(checkStore.StaffId),
                        checkStore.Time, detailDtos);
                    result.Add(dto);
                    count += 1;
                    if (count >= limit) break;
                }
            }
            else
            {
                CheckStore member = checkStores.Where(s => s.Id == id).FirstOrDefault();
                List<CheckStore> subList = checkStores.GetRange(checkStores.IndexOf(member), limit);
                foreach (CheckStore checkStore in subList)
                {
                    List<CheckStoreDetail> details = db.CheckStoreDetail.Where(s => s.CheckStoreInforId == checkStore.Id).ToList();
                    List<TransactionMaterialAmountDto> detailDtos = new List<TransactionMaterialAmountDto>();
                    foreach (CheckStoreDetail detail in details)
                    {
                        TransactionMaterialAmountDto detailDto = new TransactionMaterialAmountDto(
                            materialRepository.getSimpleMaterial(detail.MaterialId), detail.Amount.Value);
                        detailDtos.Add(detailDto);
                    }
                    CheckStoreDto dto = new CheckStoreDto(checkStore.Id, storeRepository.getSimpleStoreDto(checkStore.StoreId),
                        staffRepository.getSimpleStaff(checkStore.StaffId),
                        checkStore.Time, detailDtos);
                    result.Add(dto);
                }
            }*/
            return result;
        }

        internal void addCheckStore(CheckStoreRequestDto dto)
        {
            CheckStore checkStore = new CheckStore();
            checkStore.Id = dto.id;
            checkStore.StaffId = long.Parse(dto.staffId);
            checkStore.StoreId = dto.storeId;
            checkStore.Time = stampConvert.convertTimeStamp(dto.time);

            db.CheckStore.Add(checkStore);
            db.SaveChanges();

            int id = db.CheckStore.Where(s => s.Time == checkStore.Time).FirstOrDefault().Id;
            foreach (TransactionMaterialAmountDto dto1 in dto.detail)
            {
                CheckStoreDetail checkStoreDetail = new CheckStoreDetail();
                checkStoreDetail.Amount = dto1.materialAmount;
                checkStoreDetail.CheckStoreInforId = id;
                checkStoreDetail.MaterialId = int.Parse(dto1.material.id);

                db.CheckStoreDetail.Add(checkStoreDetail);
            }
            db.SaveChanges();
        }
    }
}
