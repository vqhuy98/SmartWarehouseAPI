using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWhRestApi.Models.ViewModels;
using SWhRestApi.Repository;

namespace SWhRestApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "NV,QL")]
    [Route("api/transaction")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private TransactionRepository transactionRepository = new TransactionRepository();
        private TimeStampConvert stampConvert = new TimeStampConvert();
        /* 
         // GET: api/transactions
         public IQueryable<transaction> Gettransactions()
         {
             return db.transactions;
         }*/

        [Route("{storeId}")]
        [HttpGet]
        public dynamic Gettransaction(string storeId, string typeId = null, double startDate = 0, double endDate = 0, int id = 0, int limit = 100)
        {

            List<TransactionDto> result = transactionRepository.getTransactionDtos(storeId, typeId, startDate, endDate, id, limit);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("EC/{storeId}")]
        [HttpGet]
        public dynamic GettransactionEC(string storeId)
        {

            List<TransactionDto> result = transactionRepository.getTransactionDtosEC(storeId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/transactions/5
        [HttpPut("{id}")]
        public dynamic Puttransaction(string id, [FromBody] String statusId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TransactionDto result = new TransactionDto();
            try
            {
                result = transactionRepository.updateStateTransaction(id, statusId);
            }
            catch (Exception e)
            {
                if (!transactionRepository.transactionExists(int.Parse(id)))
                {
                    return NotFound();
                }
                else
                {
                    throw e;
                }
            }

            return Ok(result);
        }

        
        // POST: api/transactions
        [HttpPost]
        public dynamic Posttransaction([FromBody] TransactionRequestDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                transactionRepository.addtransaction(transactionDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(true);
        }


        
    }
}