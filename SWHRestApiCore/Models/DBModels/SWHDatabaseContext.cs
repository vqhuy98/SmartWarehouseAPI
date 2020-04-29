using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SWHRestApiCore.Models.DBModels
{
    public partial class SWHDatabaseContext : DbContext
    {
        public SWHDatabaseContext()
        {
        }

        public SWHDatabaseContext(DbContextOptions<SWHDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CheckStore> CheckStore { get; set; }
        public virtual DbSet<CheckStoreDetail> CheckStoreDetail { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<MaterialType> MaterialType { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<StoreMaterial> StoreMaterial { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<TransactionDetail> TransactionDetail { get; set; }
        public virtual DbSet<TransactionType> TransactionType { get; set; }

        // Unable to generate entity type for table 'dbo.balance_transaction'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=swhdb.database.windows.net;Database=SWHDatabase;Trusted_Connection=False;user Id=adminSWhDb;password=vuquanghuy123!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<CheckStore>(entity =>
            {
                entity.ToTable("check_store");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.StoreId)
                    .IsRequired()
                    .HasColumnName("store_id")
                    .HasMaxLength(50);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.CheckStore)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_check_store_infor_staff_infor");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.CheckStore)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Check store infor_Store infor");
            });

            modelBuilder.Entity<CheckStoreDetail>(entity =>
            {
                entity.HasKey(e => new { e.CheckStoreInforId, e.MaterialId })
                    .HasName("PK_Check store");

                entity.ToTable("check_store_detail");

                entity.Property(e => e.CheckStoreInforId).HasColumnName("check_store_infor_id");

                entity.Property(e => e.MaterialId).HasColumnName("material_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.HasOne(d => d.CheckStoreInfor)
                    .WithMany(p => p.CheckStoreDetail)
                    .HasForeignKey(d => d.CheckStoreInforId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Check store_Check store infor");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.CheckStoreDetail)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Check store_Material");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("material");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Barcode).HasColumnName("barcode");

                entity.Property(e => e.ChangeUnit)
                    .IsRequired()
                    .HasColumnName("change_unit")
                    .HasMaxLength(50);

                entity.Property(e => e.Exp)
                    .HasColumnName("exp")
                    .HasMaxLength(50);

                entity.Property(e => e.MainUnit)
                    .HasColumnName("main_unit")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(250);

                entity.Property(e => e.StatusId)
                    .IsRequired()
                    .HasColumnName("status_id")
                    .HasMaxLength(50);

                entity.Property(e => e.SupplierInforId)
                    .IsRequired()
                    .HasColumnName("supplier_infor_id")
                    .HasMaxLength(50);

                entity.Property(e => e.TypeId)
                    .IsRequired()
                    .HasColumnName("type_id")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_material_status");

                entity.HasOne(d => d.SupplierInfor)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.SupplierInforId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Material_Supplier infor");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Material_Material type");
            });

            modelBuilder.Entity<MaterialType>(entity =>
            {
                entity.ToTable("material_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("position");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AuthToken)
                    .IsRequired()
                    .HasColumnName("auth_token");

                entity.Property(e => e.DeviceToken).HasColumnName("device_token");

                entity.Property(e => e.Gmail)
                    .HasColumnName("gmail")
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.PicUrl)
                    .IsRequired()
                    .HasColumnName("pic_url")
                    .HasMaxLength(250);

                entity.Property(e => e.PositionId)
                    .IsRequired()
                    .HasColumnName("position_id")
                    .HasMaxLength(50);

                entity.Property(e => e.StatusId)
                    .IsRequired()
                    .HasColumnName("status_id")
                    .HasMaxLength(50);

                entity.Property(e => e.StoreId)
                    .IsRequired()
                    .HasColumnName("store_id")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_staff_infor_position1");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_staff_status1");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_staff_store");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("store");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(250);

                entity.Property(e => e.StatusId)
                    .IsRequired()
                    .HasColumnName("status_id")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Store)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_store_status");
            });

            modelBuilder.Entity<StoreMaterial>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.MaterialId });

                entity.ToTable("store_material");

                entity.Property(e => e.StoreId)
                    .HasColumnName("store_id")
                    .HasMaxLength(50);

                entity.Property(e => e.MaterialId).HasColumnName("material_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Unit)
                    .HasColumnName("unit")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.StoreMaterial)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store's Material_Material");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreMaterial)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store-Goods_Store_Info");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("supplier");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ExchangeStoreId)
                    .HasColumnName("exchange_store_id")
                    .HasMaxLength(50);

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.StatusId)
                    .IsRequired()
                    .HasColumnName("status_id")
                    .HasMaxLength(50);

                entity.Property(e => e.StoreId)
                    .IsRequired()
                    .HasColumnName("store_id")
                    .HasMaxLength(50);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TransactionTypeId)
                    .IsRequired()
                    .HasColumnName("transaction_type_id")
                    .HasMaxLength(50);

                entity.HasOne(d => d.ExchangeStore)
                    .WithMany(p => p.TransactionExchangeStore)
                    .HasForeignKey(d => d.ExchangeStoreId)
                    .HasConstraintName("FK_transaction_store");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction infor_Staff infor");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_transaction_status");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TransactionStore)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction infor_Store infor");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_transaction_infor_transaction_type");
            });

            modelBuilder.Entity<TransactionDetail>(entity =>
            {
                entity.HasKey(e => new { e.TransId, e.MaterialId })
                    .HasName("PK_Transaction");

                entity.ToTable("transaction_detail");

                entity.Property(e => e.TransId).HasColumnName("trans_id");

                entity.Property(e => e.MaterialId).HasColumnName("material_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Unit)
                    .HasColumnName("unit")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.TransactionDetail)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Material");

                entity.HasOne(d => d.Trans)
                    .WithMany(p => p.TransactionDetail)
                    .HasForeignKey(d => d.TransId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Transaction infor");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.ToTable("transaction_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });
        }
    }
}
