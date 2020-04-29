using Newtonsoft.Json;
using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using SWHRestApiCore.Models.DBModels;
using SWHRestApiCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace SWhRestApi.Repository
{
    public class TransactionRepository
    {
        private SWHDatabaseContext db = new SWHDatabaseContext();
        private MaterialRepository materialRepository = new MaterialRepository();
        private StoreRepository storeRepository = new StoreRepository();
        private StaffRepository staffRepository = new StaffRepository();
        private TypeRepository typeRepository = new TypeRepository();
        private StatusRepository statusRepository = new StatusRepository();
        private TimeStampConvert stampConvert = new TimeStampConvert();
        String serverKey = "AAAAylW3cKg:APA91bHfIdu52ENnGpxY7saK8iUrWs7mXXa5CxIIbOPMjz1MXPqG7gEjBZGRPjWi31KscmRob1E4Q4CfkwS8qkheSnyaYtMKvJRmOLWym0rNhaWgFm48hEtJOt5P7A7vczjdfzR4tRJY";
        String url = "https://fcm.googleapis.com/fcm/send";


        public List<TransactionDto> getTransactionDtos(string storeId, string typeId, double startDate, double endDate, int id, int limit)
        {
            List<TransactionDto> result = new List<TransactionDto>();
            List<Transaction> transactions = new List<Transaction>();
            if (typeId == null && startDate == 0 && endDate == 0)
            {
                transactions = db.Transaction.Where(s => s.StoreId.ToLower().Equals(storeId.ToLower())).ToList();
            }
            else if (startDate == 0 && endDate == 0)
            {
                transactions = db.Transaction.Where(s => s.StoreId.ToLower().Equals(storeId.ToLower()) && s.TransactionTypeId.ToLower().Equals(typeId.ToLower())).ToList();
            }
            else
            {
                transactions = db.Transaction.Where(s => s.StoreId.ToLower().Equals(storeId.ToLower()) && s.TransactionTypeId.ToLower().Equals(typeId.ToLower()) &&
                startDate <= stampConvert.convertDateTime(s.Time) && stampConvert.convertDateTime(s.Time) <= endDate).ToList();
            }
            if (id == 0)
            {
                int count = 0;
                foreach (Transaction transaction in transactions)
                {
                    List<TransactionDetail> details = db.TransactionDetail.Where(s => s.TransId == transaction.Id).ToList();
                    List<TransactionMaterialAmountDto> detailDtos = new List<TransactionMaterialAmountDto>();
                    foreach (TransactionDetail detail in details)
                    {
                        TransactionMaterialAmountDto detailDto = new TransactionMaterialAmountDto(
                            materialRepository.getSimpleMaterial(detail.MaterialId), detail.Amount,detail.Unit);
                        detailDtos.Add(detailDto);
                    }
                    TransactionDto dto = new TransactionDto(transaction.Id, storeRepository.getSimpleStoreDto(transaction.ExchangeStoreId),
                        storeRepository.getSimpleStoreDto(transaction.StoreId),
                        transaction.Time, staffRepository.getSimpleStaff(transaction.StaffId),
                        typeRepository.getTransactionType(transaction.TransactionTypeId),
                        statusRepository.getStatus(transaction.StatusId), detailDtos);
                    result.Add(dto);
                    count += 1;
                    if (count >= limit) break;
                }
            }
            else
            {
                Transaction member = transactions.Where(s => s.Id == id).FirstOrDefault();
                List<Transaction> subList = transactions.GetRange(transactions.IndexOf(member), limit);
                foreach (Transaction transaction in subList)
                {
                    List<TransactionDetail> details = db.TransactionDetail.Where(s => s.TransId == transaction.Id).ToList();
                    List<TransactionMaterialAmountDto> detailDtos = new List<TransactionMaterialAmountDto>();
                    foreach (TransactionDetail detail in details)
                    {
                        TransactionMaterialAmountDto detailDto = new TransactionMaterialAmountDto(
                            materialRepository.getSimpleMaterial(detail.MaterialId), detail.Amount,detail.Unit);
                        detailDtos.Add(detailDto);
                    }
                    TransactionDto dto = new TransactionDto(transaction.Id, storeRepository.getSimpleStoreDto(transaction.ExchangeStoreId),
                        storeRepository.getSimpleStoreDto(transaction.StoreId),
                        transaction.Time, staffRepository.getSimpleStaff(transaction.StaffId),
                        typeRepository.getTransactionType(transaction.TransactionTypeId),
                        statusRepository.getStatus(transaction.StatusId), detailDtos);
                    result.Add(dto);
                }
            }
            return result;
        }

        internal List<TransactionDto> getTransactionDtosEC(string storeId)
        {
            List<TransactionDto> result = new List<TransactionDto>();
            List<Transaction> transactions = new List<Transaction>();
            transactions = db.Transaction.Where(s => s.ExchangeStoreId == storeId && s.StatusId.Equals("ĐT")).ToList();
            foreach (Transaction transaction in transactions)
            {
                List<TransactionDetail> details = db.TransactionDetail.Where(s => s.TransId == transaction.Id).ToList();
                List<TransactionMaterialAmountDto> detailDtos = new List<TransactionMaterialAmountDto>();
                foreach (TransactionDetail detail in details)
                {
                    TransactionMaterialAmountDto detailDto = new TransactionMaterialAmountDto(
                        materialRepository.getSimpleMaterial(detail.MaterialId), detail.Amount,detail.Unit);
                    detailDtos.Add(detailDto);
                }
                TransactionDto dto = new TransactionDto(transaction.Id, storeRepository.getSimpleStoreDto(transaction.ExchangeStoreId),
                    storeRepository.getSimpleStoreDto(transaction.StoreId),
                    transaction.Time, staffRepository.getSimpleStaff(transaction.StaffId),
                    typeRepository.getTransactionType(transaction.TransactionTypeId),
                    statusRepository.getStatus(transaction.StatusId), detailDtos);
                result.Add(dto);
            }
            return result;
        }

        private TransactionDto GetTransactionDto(Transaction transaction)
        {
            List<TransactionDetail> details = transaction.TransactionDetail.ToList();
            List<TransactionMaterialAmountDto> detailDtos = new List<TransactionMaterialAmountDto>();
            foreach (TransactionDetail detail in details)
            {
                TransactionMaterialAmountDto detailDto = new TransactionMaterialAmountDto(
                    materialRepository.getSimpleMaterial(detail.MaterialId), detail.Amount,detail.Unit);
                detailDtos.Add(detailDto);
            }
            TransactionDto dto = new TransactionDto(transaction.Id, storeRepository.getSimpleStoreDto(transaction.ExchangeStoreId),
                storeRepository.getSimpleStoreDto(transaction.StoreId),
                transaction.Time, staffRepository.getSimpleStaff(transaction.StaffId),
                typeRepository.getTransactionType(transaction.TransactionTypeId),
                statusRepository.getStatus(transaction.StatusId), detailDtos);
            return dto;
        }

        internal void addtransaction(TransactionRequestDto dtos)
        {
            //for transaction table
            Transaction transaction = new Transaction();
            transaction.ExchangeStoreId = dtos.exchangeStoreId;
            transaction.StoreId = dtos.storeId;
            transaction.Time = stampConvert.convertTimeStamp(dtos.time);
            transaction.StaffId = dtos.staffId;
            transaction.TransactionTypeId = dtos.transactionTypeId;
            transaction.StatusId = dtos.statusId;
            db.Transaction.Add(transaction);
            db.SaveChanges();
            if (dtos.transactionTypeId == "IM01")
            {
                Transaction transa = db.Transaction.FirstOrDefault(s => s.StoreId == transaction.ExchangeStoreId &&
                    s.ExchangeStoreId == transaction.StoreId && s.StatusId == "ĐT");
                if (transa != null)
                {
                        transa.StatusId = "HT";
                        db.Entry(transa).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();
                }
            }

            //for transaction detail table

            Transaction tran = db.Transaction.Where(s => s.Time == transaction.Time).FirstOrDefault();
            foreach (var dto in dtos.detail)
            {
                //transaction detail
                TransactionDetail detail = new TransactionDetail();
                detail.TransId = tran.Id;
                detail.MaterialId = int.Parse(dto.material.id);
                detail.Amount = dto.materialAmount;
                detail.Unit = dto.unit;
                db.TransactionDetail.Add(detail);
                db.SaveChanges();

                //store material
                String[] list = new string[] { "EC", "EX01", "EX02" };
                if (list.Contains(tran.TransactionTypeId))
                {
                    StoreMaterial storeMaterial = db.StoreMaterial.First(s => s.StoreId == tran.StoreId &&
                    s.MaterialId == detail.MaterialId);
                    storeMaterial.Amount = storeMaterial.Amount - detail.Amount;
                    db.Entry(storeMaterial).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                }
                else if (dtos.transactionTypeId == "IM01")
                {
                    StoreMaterial storeMaterial = db.StoreMaterial.FirstOrDefault(s => s.StoreId == dtos.storeId &&
                    s.MaterialId == detail.MaterialId);
                    if (storeMaterial != null)
                    {
                        storeMaterial.Amount = storeMaterial.Amount + detail.Amount;
                        db.Entry(storeMaterial).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        StoreMaterial storem = new StoreMaterial();
                        storem.MaterialId = detail.MaterialId;
                        storem.StoreId = dtos.storeId;
                        storem.Amount = detail.Amount;
                        storem.Unit = detail.Unit;
                        db.StoreMaterial.Add(storem);
                        db.SaveChanges();
                    }
                }
                else if (dtos.transactionTypeId == "IM02")
                {
                    StoreMaterial storeMaterial = db.StoreMaterial.FirstOrDefault(s => s.StoreId == dtos.storeId &&
                    s.MaterialId == detail.MaterialId);
                    if (storeMaterial != null)
                    {
                        storeMaterial.Amount = storeMaterial.Amount + detail.Amount;
                        db.Entry(storeMaterial).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        StoreMaterial storem = new StoreMaterial();
                        storem.MaterialId = detail.MaterialId;
                        storem.StoreId = dtos.storeId;
                        storem.Amount = detail.Amount;
                        storem.Unit = detail.Unit;
                        db.StoreMaterial.Add(storem);
                        db.SaveChanges();
                    }
                }
                else if (dtos.transactionTypeId == "IMAF")
                {
                    StoreMaterial storeMaterial = db.StoreMaterial.First(s => s.StoreId == dtos.storeId &&
                    s.MaterialId == detail.MaterialId);
                    storeMaterial.Amount = storeMaterial.Amount + detail.Amount;
                    db.Entry(storeMaterial).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                }

            }
            if (dtos.transactionTypeId == "EC")
            {
                Transaction trans = db.Transaction.Where(s => s.Time == transaction.Time).FirstOrDefault();
                TransactionDto dto = GetTransactionDto(trans);
                Object result = sendCloudMessageAsync(dtos.exchangeStoreId, dto,"Có giao dịch mới", "Có giao dịch mới từ kho "+ storeRepository.getSimpleStoreDto(dto.store.id).name);
            }
            if (dtos.transactionTypeId == "IM01")
            {
                Transaction trans = db.Transaction.Where(s => s.Time == transaction.Time).FirstOrDefault();
                TransactionDto dto = GetTransactionDto(trans);
                Object result = sendCloudMessageAsync(dtos.exchangeStoreId, dto, "Hoàn thành đơn hàng", "hoàn thành đơn hàng tới kho " + storeRepository.getSimpleStoreDto(dto.store.id).name);
            }
        }

        internal TransactionDto updateStateTransaction(string id, String statusId)
        {
            Transaction transaction = db.Transaction.Where(s => s.Id == int.Parse(id)).FirstOrDefault();
            if (transaction != null)
            {
                transaction.StatusId = statusId;
                db.Entry(transaction).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                db.SaveChanges();
            }
            List<TransactionDetail> details = db.TransactionDetail.Where(s => s.TransId == transaction.Id).ToList();
            List<TransactionMaterialAmountDto> detailDtos = new List<TransactionMaterialAmountDto>();
            foreach (TransactionDetail detail in details)
            {
                TransactionMaterialAmountDto detailDto = new TransactionMaterialAmountDto(
                    materialRepository.getSimpleMaterial(detail.MaterialId), detail.Amount,detail.Unit);
                detailDtos.Add(detailDto);
            }
            TransactionDto result = new TransactionDto(transaction.Id, storeRepository.getSimpleStoreDto(transaction.ExchangeStoreId),
                storeRepository.getSimpleStoreDto(transaction.StoreId),
                transaction.Time, staffRepository.getSimpleStaff(transaction.StaffId),
                typeRepository.getTransactionType(transaction.TransactionTypeId),
                statusRepository.getStatus(transaction.StatusId), detailDtos);
            return result;
        }


        public bool transactionExists(int id)
        {
            return db.Transaction.Count(e => e.Id == id) > 0;
        }

        private async System.Threading.Tasks.Task<Object> sendCloudMessageAsync(String storeId, TransactionDto dto,String header,String content)
        {

            List<Staff> stafts = db.Staff.Where(s => s.StoreId == storeId && s.DeviceToken != null).ToList();
            if (stafts.Count == 0)
            {
                return false;
            }
            List<string> deviceTokens = new List<string>();
            foreach (Staff a in stafts)
            {
                deviceTokens.Add(a.DeviceToken);
            }

            DataMessage notif = new DataMessage()
            {
                registration_ids = deviceTokens,
                data = dto,
                notification = new Notification(header,content)
            };
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.TryAddWithoutValidation("Authorization", "key =" + serverKey);
            String jsonMessage = Newtonsoft.Json.JsonConvert.SerializeObject(notif);
            request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                result = await client.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        string data = await result.Content.ReadAsStringAsync();
                        SendNotificationResponseDto routes_list = JsonConvert.DeserializeObject<SendNotificationResponseDto>(data);
                        if (routes_list.success == 0)
                        {
                            return false;
                        }
                        return true;
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
            }
            return false;
        }
    }
}