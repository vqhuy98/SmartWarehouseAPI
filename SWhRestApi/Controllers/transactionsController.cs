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
    public class transactionsController : ApiController
    {
        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        private TransactionRepository transactionRepository = new TransactionRepository();
        private TimeStampConvert stampConvert = new TimeStampConvert();
        /*
        // GET: api/transactions
        public IQueryable<transaction> Gettransactions()
        {
            return db.transactions;
        }
        */
        // GET: api/transactions/5
        [Route("api/transactions/{storeId}")]
        [ResponseType(typeof(List<TransactionDto>))]
        public IHttpActionResult Gettransaction(string storeId, string typeId = null,double startDate=0, double endDate=0,int id = 0, int limit = 50)
        {
            
            List<TransactionDto> result = transactionRepository.getTransactionDtos(storeId, typeId, startDate, endDate, id, limit);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        /*
        // PUT: api/transactions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttransaction(string id, transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (int.Parse(id) != transaction.id)
            {
                return BadRequest();
            }

            db.Entry(transaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!transactionExists(int.Parse(id)))
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
        
        
        // POST: api/transactions
        [ResponseType(typeof(TransactionDto))]
        public IHttpActionResult Posttransaction(TransactionRequestDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.transactions.Add(transaction);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (transactionExists(transaction.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }
        */
        /*
        // DELETE: api/transactions/5
        [ResponseType(typeof(transaction))]
        public IHttpActionResult Deletetransaction(string id)
        {
            transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }

            db.transactions.Remove(transaction);
            db.SaveChanges();

            return Ok(transaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        */
        private bool transactionExists(int id)
        {
            return db.transactions.Count(e => e.id == id) > 0;
        }
        
    }
}