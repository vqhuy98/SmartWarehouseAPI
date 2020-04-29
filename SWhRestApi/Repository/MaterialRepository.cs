using SWhRestApi.Models;
using SWhRestApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWhRestApi.Repository
{
    public class MaterialRepository
    {
        private SWHDatabaseEntities db = new SWHDatabaseEntities();
        private StatusRepository statusRepository = new StatusRepository();
        private TypeRepository materialTypeRepository = new TypeRepository();
        private SupplierRepository supplierRepository = new SupplierRepository();

        public MaterialRepository() { }

        public MaterialDto getMaterial(int id)
        {
            material m = db.materials.Where(s => s.id == id).FirstOrDefault();
            MaterialDto materialDto = new MaterialDto(m.id, m.name, supplierRepository.getSupplier(m.supplier_infor_id), m.barcode,
                    materialTypeRepository.getMaterialType(m.type_id), m.main_unit, m.change_unit,
                    statusRepository.getStatus(m.status_id));
            return materialDto;
        }

        public DetailDto getSimpleMaterial(int id)
        {
            material m = db.materials.Where(s => s.id == id).FirstOrDefault();
            DetailDto materialDto = new DetailDto(m.id.ToString(), m.name);
            return materialDto;
        }


    }
}