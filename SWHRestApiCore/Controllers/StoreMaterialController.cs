using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using SWhRestApi.Repository;

namespace SWhRestApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "NV,QL")]
    [Route("api/storeMaterial")]
    [ApiController]
    public class StoreMaterialController : ControllerBase
    {
        private StoreMaterialRepository storeMaterialRepository = new StoreMaterialRepository();

        // GET: api/StoreMaterial/5
        [Route("{storeId}")]
        [HttpGet]
        public dynamic Getstore_material(string storeId)
        {
            List<StoreMaterialDto> storeMaterialDtos = storeMaterialRepository.getStoreMaterials(storeId);
            if (storeMaterialDtos == null)
            {
                return NotFound();
            }

            return Ok(storeMaterialDtos);
        }

        // PUT: api/StoreMaterial/5
        [Route("{StoreId}")]
        [HttpPut]
        public dynamic Putstore_material(string storeId, [FromBody] StoreMaterialRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (storeId != requestDto.storeId)
            {
                return BadRequest();
            }

            try
            {
                storeMaterialRepository.updateStoreMaterial(storeId, requestDto);
            }
            catch (Exception e)
            {
                if (!storeMaterialRepository.store_materialExists(requestDto))
                {
                    return NotFound();
                }
                else
                {
                    throw e;
                }
            }

            return Ok(requestDto);
        }

        // POST: api/StoreMaterial
        [HttpPost]
        public dynamic Poststore_material([FromBody] StoreMaterialRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                storeMaterialRepository.addStoreMaterial(requestDto);
            }
            catch (Exception e)
            {
                if (storeMaterialRepository.store_materialExists(requestDto))
                {
                    return Conflict();
                }
                else
                {
                    throw e;
                }
            }

            StoreMaterialDto storeMaterialDto = storeMaterialRepository.getStoreMaterial(requestDto);

            return Ok(storeMaterialDto);
        }

    }
}