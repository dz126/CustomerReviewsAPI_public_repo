﻿// <auto-generated />
using System;
using CustomerReviewsAPI_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomerReviewsAPI_.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CustomerReviewsAPI_.Models.Campaign", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CampaignName")
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CampaignName");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDTTM")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<string>("RequestedIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReviewUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ReviewUrl");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDTTM")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("ID");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("CustomerReviewsAPI_.Models.Company", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Address");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CompanyName");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDTTM")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<string>("POCName")
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("POCName");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("PhoneNumber");

                    b.Property<string>("RequestedIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDTTM")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("ID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("CustomerReviewsAPI_.Models.CompanyCampaign", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CampaignID")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CampaignID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("ProductID");

                    b.ToTable("CompaniesCampaigns");
                });

            modelBuilder.Entity("CustomerReviewsAPI_.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDTTM")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ProductName");

                    b.Property<string>("RequestedIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDTTM")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("ID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CustomerReviewsAPI_.Models.ProductCompany", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductsCompanies");
                });

            modelBuilder.Entity("CustomerReviewsAPI_.Models.CompanyCampaign", b =>
                {
                    b.HasOne("CustomerReviewsAPI_.Models.Campaign", "Campaign")
                        .WithMany("CompaniesCampaigns")
                        .HasForeignKey("CampaignID");

                    b.HasOne("CustomerReviewsAPI_.Models.Company", "Company")
                        .WithMany("CompaniesCampaigns")
                        .HasForeignKey("CompanyID");

                    b.HasOne("CustomerReviewsAPI_.Models.Product", "Product")
                        .WithMany("CompaniesCampaigns")
                        .HasForeignKey("ProductID");

                    b.Navigation("Campaign");

                    b.Navigation("Company");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CustomerReviewsAPI_.Models.ProductCompany", b =>
                {
                    b.HasOne("CustomerReviewsAPI_.Models.Company", "Company")
                        .WithMany("ProductsCompanies")
                        .HasForeignKey("CompanyID");

                    b.HasOne("CustomerReviewsAPI_.Models.Product", "Product")
                        .WithMany("ProductsCompanies")
                        .HasForeignKey("ProductID");

                    b.Navigation("Company");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CustomerReviewsAPI_.Models.Campaign", b =>
                {
                    b.Navigation("CompaniesCampaigns");
                });

            modelBuilder.Entity("CustomerReviewsAPI_.Models.Company", b =>
                {
                    b.Navigation("CompaniesCampaigns");

                    b.Navigation("ProductsCompanies");
                });

            modelBuilder.Entity("CustomerReviewsAPI_.Models.Product", b =>
                {
                    b.Navigation("CompaniesCampaigns");

                    b.Navigation("ProductsCompanies");
                });
#pragma warning restore 612, 618
        }
    }
}