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

namespace SWHRestApiCore.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "NV,QL")]
    [Route("api/material")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private MaterialRepository materialRepository = new MaterialRepository();
        private SupplierRepository supplierRepository = new SupplierRepository();

        [HttpGet]
        public List<MaterialDto> Getmaterials()
        {
            List<MaterialDto> dtos = materialRepository.getMaterials();
            return dtos;
        }

        [HttpGet("{id}")]
        public dynamic Getmaterial(int id)
        {
            MaterialDto materialDto = materialRepository.getMaterial(id);
            if (materialDto == null)
            {
                return NotFound();
            }
            return Ok(materialDto);
        }

        [HttpGet("supplier/{id}")]
        public List<MaterialDto> GetmaterialsBySupplier( String id)
        {
            List<MaterialDto> dtos = materialRepository.getMaterialsBySupllier(id);
            return dtos;
        }

        [HttpGet("suppliers")]
        public List<DetailDto> GetSuppliers()
        {
            List<DetailDto> dtos = supplierRepository.getSuplliers();
            return dtos;
        }

        [HttpPut("{id}")]
        public dynamic Putmaterial(int id, [FromBody] MaterialRequestDto materialDto)
        {
            MaterialDto result = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialDto.id)
            {
                return BadRequest();
            }
            try
            {
                result = materialRepository.updateMaterial(id, materialDto);
            }
            catch (Exception e)
            {
                if (!materialRepository.materialExists(id))
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

        // POST: api/materials
        [HttpPost]
        public dynamic Postmaterial([FromBody] MaterialRequestDto materialDto)
        {
            MaterialDto result = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                result = materialRepository.addMaterial(materialDto);
            }
            catch (Exception e)
            {
                if (materialRepository.materialExists(materialDto.id))
                {
                    return Conflict();
                }
                else
                {
                    throw e;
                }
            }

            return Ok(result);
        }

        // DELETE: api/materials/5
        [HttpDelete("{id}")]
        public dynamic Deletematerial(int id)
        {
            if (!materialRepository.materialExists(id))
            {
                return NotFound();
            }

            MaterialDto materialDto = materialRepository.deleteMaterial(id);
            return Ok(materialDto);
        }

    }
}