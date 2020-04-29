using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWHRestApiCore.Models.ViewModels;
using SWHRestApiCore.Repository;

namespace SWHRestApiCore.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "NV,QL")]
    [Route("api/checkStore")]
    [ApiController]
    public class CheckStoreController : ControllerBase
    {
        private CheckStoreRepository checkStoreRepository = new CheckStoreRepository();

        [Route("{storeId}")]
        [HttpGet]
        public dynamic Gettransaction(string storeId, string typeId = null, double startDate = 0, double endDate = 0, int id = 0, int limit = 50)
        {

            List<CheckStoreDto> result = checkStoreRepository.getCheckStoreDtos(storeId, startDate, endDate, id, limit);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("{storeId}")]
        [HttpPost]
        public dynamic postTransaction([FromBody] CheckStoreRequestDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                checkStoreRepository.addCheckStore(dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(true);
        }
    }
}