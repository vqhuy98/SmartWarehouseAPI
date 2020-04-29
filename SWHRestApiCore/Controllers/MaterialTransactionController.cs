using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWhRestApi.Models.ViewModels;
using SWhRestApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SWHRestApiCore.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "NV,QL")]
    [Route("api/materialTransaction")]
    [ApiController]
    public class MaterialTransactionController : ControllerBase
    {
        private MaterialTransactionRepository materialTransactionRepository = new MaterialTransactionRepository();

        [Route("{storeId}/{materialId}")]
        [HttpGet]
        public dynamic Getstore_material(string storeId, string materialId)
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
