using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class MaterialTransactionRepository
    {
        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        private TransactionRepository transactionRepository = new TransactionRepository();

        internal List<TransactionByMaterialDto> getListMaterialTransaction(string storeId, string materialId)
        {
            List<TransactionByMaterialDto> materialTransactions = new List<TransactionByMaterialDto>();
            List<TransactionDto> transactionDtos = transactionRepository.getTransactionDtos(storeId,null,0,0,0,50);
            foreach(TransactionDto transactionDto in transactionDtos)
            {
                foreach(TransactionMaterialAmountDto amountDto in transactionDto.detail)
                {
                    if(amountDto.material.id == materialId)
                    {
                        TransactionByMaterialDto materialT = new TransactionByMaterialDto(transactionDto.time,
                            transactionDto.exchangeStore, transactionDto.staff, transactionDto.transactionType, transactionDto.status,
                            amountDto.material, amountDto.materialAmount);
                        materialTransactions.Add(materialT);
                    }
                }
            }
            return materialTransactions;
        }
    }
}