using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Shop.Data.EF
{
    public partial class ShopOnlineAppContext : DbContext
    {
        public ShopOnlineAppContext()
        {
        }

        public ShopOnlineAppContext(DbContextOptions<ShopOnlineAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=PHAMQUANG2K\\SQLFLUTTER;Initial Catalog=ShopOnlineApp;Persist Security Info=True;User ID=sa;Password=quangpham2k");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => new { e.Uid, e.ProductId });

                entity.Property(e => e.Uid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("uid");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.CounterInCart).HasColumnName("counterInCart");

                entity.Property(e => e.FeeShipping).HasColumnName("feeShipping");

                entity.Property(e => e.NumberProduct).HasColumnName("numberProduct");

                entity.Property(e => e.Total).HasColumnName("total");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ImageProduct)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("imageProduct");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Original)
                    .HasMaxLength(50)
                    .HasColumnName("original");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.TypeProduct)
                    .HasMaxLength(50)
                    .HasColumnName("typeProduct");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.Property(e => e.Uid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("uid");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.ImageUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("imageUser");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone");
                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
