using SWhRestApi.Models.ViewModels;
using SWhRestApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SWhRestApi.Controllers
{
    public class MaterialTransactionController : ApiController
    {
        private MaterialTransactionRepository materialTransactionRepository = new MaterialTransactionRepository(); 
        
        [Route("api/MaterialTransaction/{storeId}/{materialId}")]
        [HttpGet]
        [ResponseType(typeof(List<TransactionByMaterialDto>))]
        public IHttpActionResult Getstore_material(string storeId, string materialId)
        {
            List<TransactionByMaterialDto> dtos = materialTransactionRepository.getListMaterialTransaction(storeId, materialId);
            if (dtos == null)
            {
                return NotFound();
            }
            
            return Ok(dtos);
        }
    }
}
