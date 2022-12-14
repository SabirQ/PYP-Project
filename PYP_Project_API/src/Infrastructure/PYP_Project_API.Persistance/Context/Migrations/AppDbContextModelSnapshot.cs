// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PYP_Project_API.Persistance.Context;

#nullable disable

namespace PYP_Project_API.Persistance.Context.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PYP_Project_API.Domain.Entities.ExcelCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ExcelCollections");
                });

            modelBuilder.Entity("PYP_Project_API.Domain.Entities.ExcelDataItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("COGS")
                        .HasColumnType("float");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DiscountBand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Discounts")
                        .HasColumnType("float");

                    b.Property<int>("ExcelCollectionId")
                        .HasColumnType("int");

                    b.Property<double>("GrossSales")
                        .HasColumnType("float");

                    b.Property<double>("ManufacturingPrice")
                        .HasColumnType("float");

                    b.Property<string>("Product")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Profit")
                        .HasColumnType("float");

                    b.Property<double>("SalePrice")
                        .HasColumnType("float");

                    b.Property<double>("Sales")
                        .HasColumnType("float");

                    b.Property<string>("Segment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("UnitsSold")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ExcelCollectionId");

                    b.ToTable("ExcelDataItems");
                });

            modelBuilder.Entity("PYP_Project_API.Domain.Entities.ExcelDataItem", b =>
                {
                    b.HasOne("PYP_Project_API.Domain.Entities.ExcelCollection", "ExcelCollection")
                        .WithMany("ExcelDataItems")
                        .HasForeignKey("ExcelCollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExcelCollection");
                });

            modelBuilder.Entity("PYP_Project_API.Domain.Entities.ExcelCollection", b =>
                {
                    b.Navigation("ExcelDataItems");
                });
#pragma warning restore 612, 618
        }
    }
}
