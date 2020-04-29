using System;
using System.Collections.Generic;

namespace SWHRestApiCore.Models.DBModels
{
    public partial class Material
    {
        public Material()
        {
            CheckStoreDetail = new HashSet<CheckStoreDetail>();
            StoreMaterial = new HashSet<StoreMaterial>();
            TransactionDetail = new HashSet<TransactionDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SupplierInforId { get; set; }
        public int Barcode { get; set; }
        public string TypeId { get; set; }
        public string MainUnit { get; set; }
        public string ChangeUnit { get; set; }
        public string StatusId { get; set; }
        public string Exp { get; set; }

        public virtual Status Status { get; set; }
        public virtual Supplier SupplierInfor { get; set; }
        public virtual MaterialType Type { get; set; }
        public virtual ICollection<CheckStoreDetail> CheckStoreDetail { get; set; }
        public virtual ICollection<StoreMaterial> StoreMaterial { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetail { get; set; }
    }
}
