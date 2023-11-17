using Microsoft.EntityFrameworkCore;
using Store_App.Helpers;

namespace Store_App.Models.DBClasses
{
    public partial class StoreAppDbContext : DbContext
    {
        public StoreAppDbContext()
        {
        }

        public StoreAppDbContext(DbContextOptions<StoreAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }

        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductToCart> ProductToCarts { get; set; }

        public virtual DbSet<ProductToCategory> ProductToCategories { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(ConfigConnectionHelper.GetConnectionString());

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.AddressId).HasName("PK__Addresse__091C2A1B47A992AA");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");
                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.PostalCode)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Street)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD797F1D1466F");

                entity.ToTable("Cart");

                entity.Property(e => e.CartId).HasColumnName("CartID");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B81984B13");

                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A585A8C6E64");

                entity.ToTable("Payment");

                entity.HasIndex(e => e.CardNumber, "UQ__Payment__A4E9FFE97D82165E").IsUnique();

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
                entity.Property(e => e.CardFirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.CardLastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.CardNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Cvv).HasColumnName("CVV");
                entity.Property(e => e.ExpirationDate).HasColumnType("date");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.PersonId).HasName("PK__Person__AA2FFB85C3D5BAAE");

                entity.ToTable("Person");

                entity.HasIndex(e => e.Email, "UQ__Person__A9D105344870F967").IsUnique();

                entity.Property(e => e.PersonId).HasColumnName("PersonID");
                entity.Property(e => e.AddressId).HasColumnName("AddressID");
                entity.Property(e => e.CartId).HasColumnName("CartID");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.HasOne(d => d.Address).WithMany(p => p.People)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__Person__AddressI__4D5F7D71");

                entity.HasOne(d => d.Cart).WithMany(p => p.People)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK__Person__CartID__4F47C5E3");

                entity.HasOne(d => d.Payment).WithMany(p => p.People)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK__Person__PaymentI__4E53A1AA");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6ED3E2B8228");

                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.ImageUrl).HasColumnName("ImageURL")
                .HasConversion<byte[]>();
                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.SaleId).HasColumnName("SaleID");
                entity.Property(e => e.Descript)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.ManufacturerInformation)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.Sku)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductToCart>(entity =>
            {
                entity.HasKey(e => e.ProdToCartId).HasName("PK__ProductT__2C6332F554587985");

                entity.ToTable("ProductToCart");

                entity.Property(e => e.ProdToCartId).HasColumnName("ProdToCartID");
                entity.Property(e => e.CartId).HasColumnName("CartID");
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Cart).WithMany(p => p.ProductToCarts)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK__ProductTo__CartI__40F9A68C");

                entity.HasOne(d => d.Product).WithMany(p => p.ProductToCarts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ProductTo__Produ__41EDCAC5");
            });

            modelBuilder.Entity<ProductToCategory>(entity =>
            {
                entity.HasKey(e => e.ProdToCatId).HasName("PK__ProductT__429919A64E8BB0D6");

                entity.ToTable("ProductToCategory");

                entity.Property(e => e.ProdToCatId).HasColumnName("ProdToCatID");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Category).WithMany(p => p.ProductToCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__ProductTo__Categ__44CA3770");

                entity.HasOne(d => d.Product).WithMany(p => p.ProductToCategories)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ProductTo__Produ__45BE5BA9");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.SaleId).HasName("PK__Sale__1EE3C41F44DF4011");

                entity.ToTable("Sale");

                entity.Property(e => e.SaleId).HasColumnName("SaleID");
                entity.Property(e => e.EndDate).HasColumnType("datetime");
                entity.Property(e => e.PercentOff).HasColumnType("decimal(5, 2)");
                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}