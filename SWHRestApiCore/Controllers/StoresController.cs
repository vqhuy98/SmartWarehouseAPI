using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using SWhRestApi.Repository;
using SWHRestApiCore.Models.ViewModels;

namespace SWhRestApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "QL,NV")]
    [Route("api/store")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private StoreRepository storeRepository = new StoreRepository();

        // GET: api/stores
        [HttpGet("stores")]
        public List<StoreDto> Getstores()
        {
            List<StoreDto> storeDtos = storeRepository.getStoreDtos();
            
            return storeDtos;
        }

        // GET: api/stores
        [HttpGet("simpleStores")]
        public List<DetailDto> GetSimpleStore()
        {
            List<DetailDto> storeDtos = storeRepository.getSimpleStore();
            return storeDtos;
        }



        // GET: api/stores/5
        [HttpGet("{id}")]
        public dynamic Getstore(string id)
        {
            StoreDto storeDto = storeRepository.getStoreDto(id);
            if (storeDto == null)
            {
                return NotFound();
            }
            return Ok(storeDto);
        }

        [HttpPut("{id}")]
        public dynamic Putstore(string id,[FromBody] StoreRequestDto storeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != storeDto.id)
            {
                return BadRequest();
            }
            try
            {
                storeRepository.updateStore(storeDto);
            }
            catch (Exception e)
            {
                if (!storeRepository.storeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw e;
                }
            }
            StoreDto result = storeRepository.getStoreDto(id);
            return Ok(result);
        }

        [HttpPost]
        public dynamic Poststore([FromBody] StoreRequestDto storeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                storeRepository.addStore(storeDto);
            }
            catch (Exception e)
            {
                if (storeRepository.storeExists(storeDto.id))
                {
                    return Conflict();
                }
                else
                {
                    throw e;
                }
            }

            StoreDto result = storeRepository.getStoreDto(storeDto.id);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public dynamic Deletestore(string id)
        {
            StoreDto store = storeRepository.deleteStore(id);
            
            if (store == null)
            {
                return NotFound();
            }
            
            return Ok(store);
        }

    }
}