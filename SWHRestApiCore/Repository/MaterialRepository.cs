using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using SWHRestApiCore.Models.DBModels;
using SWHRestApiCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class MaterialRepository
    {
        private SWHDatabaseContext db = new SWHDatabaseContext();
        private StatusRepository statusRepository = new StatusRepository();
        private TypeRepository materialTypeRepository = new TypeRepository();
        private SupplierRepository supplierRepository = new SupplierRepository();

        public MaterialRepository() { }

        public MaterialDto getMaterial(int id)
        {
            Material m = db.Material.Where(s => s.Id == id).FirstOrDefault();
            MaterialDto materialDto = new MaterialDto(m.Id, m.Name, supplierRepository.getSupplier(m.SupplierInforId), m.Barcode,
                    materialTypeRepository.getMaterialType(m.TypeId), m.ChangeUnit,
                    statusRepository.getStatus(m.StatusId),m.Exp);
            return materialDto;
        }

        internal List<MaterialDto> getMaterials()
        {
            List<Material> materials = db.Material.ToList();
            List<MaterialDto> materialDtos = new List<MaterialDto>();
            foreach (Material m in materials)
            {
                MaterialDto materialDto = getMaterial(m.Id);
                materialDtos.Add(materialDto);
            }
            return materialDtos;
        }

        internal List<MaterialDto> getMaterialsBySupllier(String id)
        {
            List<Material> materials = db.Material.Where(s => s.SupplierInforId == id && s.StatusId.Equals("CSD")).ToList();
            List<MaterialDto> materialDtos = new List<MaterialDto>();
            foreach (Material m in materials)
            {
                MaterialDto materialDto = getMaterial(m.Id);
                materialDtos.Add(materialDto);
            }
            return materialDtos;
        }

        internal MaterialDto updateMaterial(int id, MaterialRequestDto materialDto)
        {
            Material material = db.Material.Find(id);
            material.Name = materialDto.name;
            material.SupplierInforId = materialDto.supplierId;
            material.Barcode = materialDto.barcode;
            material.Id = material.Barcode;
            material.TypeId = materialDto.typeId;
            material.ChangeUnit = materialDto.changeUnit;
            material.StatusId = materialDto.statusId;
            material.Exp = materialDto.exp;

            db.Entry(material).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();
            return getMaterial(id);
        }

        public DetailDto getSimpleMaterial(int id)
        {
            Material m = db.Material.Where(s => s.Id == id).FirstOrDefault();
            DetailDto materialDto = new DetailDto(m.Id.ToString(), m.Name);
            return materialDto;
        }
        public bool materialExists(int id)
        {
            return db.Material.Count(e => e.Id == id) > 0;
        }

        internal MaterialDto addMaterial(MaterialRequestDto materialDto)
        {
            Material material = new Material();
            material.Id = materialDto.id;
            material.Name = materialDto.name;
            material.SupplierInforId = materialDto.supplierId;
            material.Barcode = materialDto.barcode;
            material.TypeId = materialDto.typeId;
            material.ChangeUnit = materialDto.changeUnit;
            material.StatusId = materialDto.statusId;
            material.Exp = materialDto.exp;

            db.Material.Add(material);
            db.SaveChanges();

            return getMaterial(materialDto.id);

        }

        internal MaterialDto deleteMaterial(int id)
        {

            Material material = db.Material.Find(id);
            material.StatusId = "NSD";

            db.Entry(material).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();
            MaterialDto materialDto = getMaterial(material.Id);
            return materialDto;
        }
    }
}