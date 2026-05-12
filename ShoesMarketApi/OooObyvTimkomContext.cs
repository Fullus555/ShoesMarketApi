using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShoesMarketApi;

public partial class OooObyvTimkomContext : DbContext
{
    public OooObyvTimkomContext()
    {
    }

    public OooObyvTimkomContext(DbContextOptions<OooObyvTimkomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<ItemQuantity> ItemQuantities { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<PickUpPoint> PickUpPoints { get; set; }

    public virtual DbSet<Producer> Producers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=OOO_obyv_timkom;Username=postgres;Password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("Employees_pkey");

            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.EmployeeRole)
                .HasMaxLength(100)
                .HasColumnName("employee_role");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Login)
                .HasMaxLength(150)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
        });

        modelBuilder.Entity<ItemQuantity>(entity =>
        {
            entity.HasKey(e => e.ItemQuantityId).HasName("Item_quantity_pkey");

            entity.ToTable("Item_quantity");

            entity.Property(e => e.ItemQuantityId).HasColumnName("item_quantity_id");
            entity.Property(e => e.ArticleNumberFk)
                .HasMaxLength(50)
                .HasColumnName("article_number_fk");
            entity.Property(e => e.OrderNumberFk).HasColumnName("order_number_fk");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.ArticleNumberFkNavigation).WithMany(p => p.ItemQuantities)
                .HasPrincipalKey(p => p.ArticleNumber)
                .HasForeignKey(d => d.ArticleNumberFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Item_quantity_article_number_fk_fkey");

            entity.HasOne(d => d.OrderNumberFkNavigation).WithMany(p => p.ItemQuantities)
                .HasForeignKey(d => d.OrderNumberFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Item_quantity_order_number_fk_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderNumber).HasName("Orders_pkey");

            entity.Property(e => e.OrderNumber).HasColumnName("order_number");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("delivery_date");
            entity.Property(e => e.FullNameOfEmployeeFk).HasColumnName("full_name_of_employee_fk");
            entity.Property(e => e.OrderDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderStatusFk).HasColumnName("order_status_fk");
            entity.Property(e => e.PickUpPointFk).HasColumnName("pick_up_point_fk");
            entity.Property(e => e.ReceiptCode).HasColumnName("receipt_code");

            entity.HasOne(d => d.FullNameOfEmployeeFkNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FullNameOfEmployeeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_full_name_of_employee_fk_fkey");

            entity.HasOne(d => d.OrderStatusFkNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatusFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_order_status_fk_fkey");

            entity.HasOne(d => d.PickUpPointFkNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PickUpPointFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_pick_up_point_fk_fkey");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.IdOrderStatus).HasName("Order_statuses_pkey");

            entity.ToTable("Order_statuses");

            entity.Property(e => e.IdOrderStatus).HasColumnName("id_order_status");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<PickUpPoint>(entity =>
        {
            entity.HasKey(e => e.IdPickUpPoints).HasName("Pick_up_points_pkey");

            entity.ToTable("Pick_up_points");

            entity.Property(e => e.IdPickUpPoints).HasColumnName("id_pick_up_points");
            entity.Property(e => e.City)
                .HasMaxLength(70)
                .HasColumnName("city");
            entity.Property(e => e.Index).HasColumnName("index");
            entity.Property(e => e.StreetAndHouse)
                .HasMaxLength(150)
                .HasColumnName("street_and_house");
        });

        modelBuilder.Entity<Producer>(entity =>
        {
            entity.HasKey(e => e.IdProducer).HasName("Producers_pkey");

            entity.Property(e => e.IdProducer).HasColumnName("id_producer");
            entity.Property(e => e.ProducerName)
                .HasMaxLength(100)
                .HasColumnName("producer_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("Products_pkey");

            entity.HasIndex(e => e.ArticleNumber, "Products_article_number_key").IsUnique();

            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.ArticleNumber)
                .HasMaxLength(20)
                .HasColumnName("article_number");
            entity.Property(e => e.CurrentDiscount).HasColumnName("current_discount");
            entity.Property(e => e.Photo)
                .HasMaxLength(50)
                .HasColumnName("photo");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.ProducerFk).HasColumnName("producer_fk");
            entity.Property(e => e.ProductCategoryFk).HasColumnName("product_category_fk");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(255)
                .HasColumnName("product_description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(40)
                .HasColumnName("product_name");
            entity.Property(e => e.QuantityInWarehouse).HasColumnName("quantity_in_warehouse");
            entity.Property(e => e.SupplierFk).HasColumnName("supplier_fk");
            entity.Property(e => e.UnitMeasurement)
                .HasMaxLength(10)
                .HasColumnName("unit_measurement");

            entity.HasOne(d => d.ProducerFkNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProducerFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_producer_fk_fkey");

            entity.HasOne(d => d.ProductCategoryFkNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_product_category_fk_fkey");

            entity.HasOne(d => d.SupplierFkNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_supplier_fk_fkey");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.IdProductCategory).HasName("Product_categories_pkey");

            entity.ToTable("Product_categories");

            entity.Property(e => e.IdProductCategory).HasColumnName("id_product_category");
            entity.Property(e => e.ProductCategory1)
                .HasMaxLength(100)
                .HasColumnName("product_category");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.IdSupplier).HasName("Suppliers_pkey");

            entity.Property(e => e.IdSupplier).HasColumnName("id_supplier");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .HasColumnName("supplier_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
