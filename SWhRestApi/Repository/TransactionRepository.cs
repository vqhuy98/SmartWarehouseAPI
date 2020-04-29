using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class TransactionRepository
    {
        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        private MaterialRepository materialRepository = new MaterialRepository();
        private StoreRepository storeRepository = new StoreRepository();
        private StaffRepository staffRepository = new StaffRepository();
        private TypeRepository typeRepository = new TypeRepository();
        private StatusRepository statusRepository = new StatusRepository();
        private TimeStampConvert stampConvert = new TimeStampConvert();

        public List<TransactionDto> getTransactionDtos(string storeId, string typeId, double startDate, double endDate, int id, int limit)
        {
            List<TransactionDto> result = new List<TransactionDto>();
            List<transaction> transactions = new List<transaction>();
            if (typeId == null)
            {
                transactions = db.transactions.Where(s => s.store_id == storeId).ToList();
            }
            else if (startDate == 0 && endDate == 0)
            {
                transactions = db.transactions.Where(s => s.store_id == storeId && s.transaction_type_id == typeId).ToList();
            }
            else
                transactions = db.transactions.Where(s => s.store_id == storeId && s.transaction_type_id == typeId &&
                stampConvert.convertTimeStamp(startDate) <= s.time && s.time <= stampConvert.convertTimeStamp(endDate)).ToList();
            if (id == 0)
            {
                int count = 0;
                foreach (transaction transaction in transactions)
                {
                    List<transaction_detail> details = db.transaction_detail.Where(s => s.trans_id == transaction.id).ToList();
                    List<TransactionMaterialAmountDto> detailDtos = new List<TransactionMaterialAmountDto>();
                    foreach (transaction_detail detail in details)
                    {
                        TransactionMaterialAmountDto detailDto = new TransactionMaterialAmountDto(
                            materialRepository.getSimpleMaterial(detail.material_id), detail.amount);
                        detailDtos.Add(detailDto);
                    }
                    TransactionDto dto = new TransactionDto(transaction.id, storeRepository.getSimpleStoreDto(transaction.exchange_store_id),
                        storeRepository.getSimpleStoreDto(transaction.store_id),
                        transaction.time, staffRepository.getSimpleStaff(transaction.staff.auth_token.ToString()),
                        typeRepository.getTransactionType(transaction.transaction_type_id),
                        statusRepository.getStatus(transaction.status_id), detailDtos);
                    result.Add(dto);
                    count += 1;
                    if (count >= limit) break;
                }
            }
            else
            {
                transaction member = transactions.Where(s => s.id == id).FirstOrDefault();
                List<transaction> subList = transactions.GetRange(transactions.IndexOf(member), limit);
                foreach (transaction transaction in subList)
                {
                    List<transaction_detail> details = db.transaction_detail.Where(s => s.trans_id == transaction.id).ToList();
                    List<TransactionMaterialAmountDto> detailDtos = new List<TransactionMaterialAmountDto>();
                    foreach (transaction_detail detail in details)
                    {
                        TransactionMaterialAmountDto detailDto = new TransactionMaterialAmountDto(
                            materialRepository.getSimpleMaterial(detail.material_id), detail.amount);
                        detailDtos.Add(detailDto);
                    }
                    TransactionDto dto = new TransactionDto(transaction.id, storeRepository.getSimpleStoreDto(transaction.exchange_store_id),
                        storeRepository.getSimpleStoreDto(transaction.store_id),
                        transaction.time, staffRepository.getSimpleStaff(transaction.staff_id.ToString()),
                        typeRepository.getTransactionType(transaction.transaction_type_id),
                        statusRepository.getStatus(transaction.status_id), detailDtos);
                    result.Add(dto);
                }
            }
            return result;
        }
        public transaction convertTransaction(TransactionRequestDto dtos)
        {
            transaction transaction = new transaction();
            transaction.exchange_store_id = dtos.exchangeStoreId;
            transaction.store_id = dtos.storeId;
            transaction.time = stampConvert.convertTimeStamp(dtos.time);
            transaction.staff_id = dtos.staffId;
            transaction.transaction_type_id = dtos.transactionTypeId;
            transaction.status_id = dtos.statusId;

            foreach (var dto in dtos.detail)
            {
                transaction_detail detail = new transaction_detail();
                detail.trans_id = dtos.id;
                detail.material_id = int.Parse(dto.material.id);
                detail.amount = dto.materialAmount;
                db.transaction_detail.Add(detail);
                String[] list = new string[] {"EC","EX01","EX02","IM02"};
            
                if(dtos.transactionTypeId == "EC")
                {
                    store_material storeMaterial = db.store_material.Where(s => s.store_id == dtos.storeId &&
                    s.material_id == detail.material_id).FirstOrDefault();
                    storeMaterial.amount = storeMaterial.amount - detail.amount;
                    db.Entry(storeMaterial).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return transaction;

        }
    }
}