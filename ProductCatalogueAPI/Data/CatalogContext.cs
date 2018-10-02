using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogueAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogueAPI.Data
{
    public class CatalogContext:DbContext
    {
    public CatalogContext(DbContextOptions option):
            base(option)
        {
        }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet <CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }

        //DBInjection, Overriding applied on inheritance
        //this is to tell the base class on model creating to recognize the defined tables
        //conceptual model building on configration for the life time of the application
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity <CatalogBrand>(ConfigureCatalogBrand);
            modelBuilder.Entity <CatalogType>(ConfigureCatalogType);
            modelBuilder.Entity<CatalogItem>(ConfigureCatalogItem);
        }

        private void ConfigureCatalogType
            (EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogType");
            builder.Property(c=> c.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("Catalog_Type_Hilo");
            builder.Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(100);
        }
        private void ConfigureCatalogBrand
            (EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("CatalogBrand");
            builder.Property(c => c.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("Catalog_Brand_Hilo");
            builder.Property(c => c.Brand)
                .IsRequired()
                .HasMaxLength(100);
        }
        private void ConfigureCatalogItem
            (EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("CatalogItem");
            builder.Property(c => c.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("Catalog_Hilo");
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Price)
                .IsRequired();

            ////if these were not written here, that means it is not required.
            //builder.Property(c => c.Description)
            //    .IsRequired(false)
            //    .HasMaxLength(100);
            builder.Property(c => c.PictureURL)
                .IsRequired(false)
                .HasMaxLength(100);
            builder.HasOne(c => c.CatalogBrand)
                .WithMany()
                .HasForeignKey(c => c.CatalogBrandId);
            builder.HasOne(c => c.CatalogType)
                .WithMany()
                .HasForeignKey(c => c.CatalogTypeId);
        }
    }
}
