using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace repairServiceCW.Models
{
    public partial class repair_serviceContext : DbContext
    {
        public repair_serviceContext()
        {
        }

        public repair_serviceContext(DbContextOptions<repair_serviceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ElementType> ElementTypes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderElement> OrderElements { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=Gato1_otaG990;database=repair_service", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<ElementType>(entity =>
            {
                entity.HasKey(e => e.ElementCode)
                    .HasName("PRIMARY");

                entity.ToTable("element_types");

                entity.HasIndex(e => e.ElementType1, "Название типа")
                    .IsUnique();

                entity.Property(e => e.ElementCode).HasColumnName("element_code");

                entity.Property(e => e.ElementType1)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("element_type");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasIndex(e => e.CodeStatus, "IX_order_status");

                entity.HasIndex(e => e.OrderCode, "Код заказа")
                    .IsUnique();

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.OrderCode)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("order_code");

                entity.Property(e => e.OrderDescription)
                    .HasMaxLength(1000)
                    .HasColumnName("order_description")
                    .HasDefaultValueSql("'Описание не предоставлено'");

                entity.Property(e => e.OrderDevice)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("order_device");

                entity.Property(e => e.OrderDeviceModel)
                    .HasMaxLength(30)
                    .HasColumnName("order_device_model")
                    .HasDefaultValueSql("'Не указана'");

                entity.Property(e => e.OrderTakeDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_take_date")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.CodeStatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CodeStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_status");
            });

            modelBuilder.Entity<OrderElement>(entity =>
            {
                entity.HasKey(e => new { e.ElementId, e.IdOrder })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("order_element");

                entity.HasIndex(e => e.CodeElement, "IX_element_type");

                entity.HasIndex(e => e.IdOrder, "order_elements");

                entity.Property(e => e.ElementId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("element_id");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.CodeElement).HasColumnName("code_element");

                entity.Property(e => e.ElementEndDate)
                    .HasColumnType("date")
                    .HasColumnName("element_end_date")
                    .HasDefaultValueSql("'1970-01-01'");

                entity.Property(e => e.ElementName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("element_name");

                entity.Property(e => e.ElementPrice).HasColumnName("element_price");

                entity.Property(e => e.ElementQuantity).HasColumnName("element_quantity");

                entity.HasOne(d => d.CodeElementNavigation)
                    .WithMany(p => p.OrderElements)
                    .HasForeignKey(d => d.CodeElement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("element_type");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderElements)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_elements");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasKey(e => e.StatusCode)
                    .HasName("PRIMARY");

                entity.ToTable("order_statuses");

                entity.HasIndex(e => e.StatusName, "Название статуса")
                    .IsUnique();

                entity.Property(e => e.StatusCode).HasColumnName("status_code");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("status_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
