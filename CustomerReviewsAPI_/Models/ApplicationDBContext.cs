
using Microsoft.EntityFrameworkCore;
using System;

namespace CustomerReviewsAPI_.Models
{

    #region DB Context
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Product-Campaign
                    //modelBuilder.Entity<ProductCampaign>()
                    //    .HasOne(p => p.Product)
                    //    .WithMany(e => e.ProductCampaigns)
                    //    .HasForeignKey(k => k.ProductID);

                    //modelBuilder.Entity<ProductCampaign>()
                    //.HasOne(e => e.Campaign)
                    //.WithMany(e => e.ProductCampaigns)
                    //.HasForeignKey(k => k.CampaignID);
            #endregion

            #region Product-Company
            
                    modelBuilder.Entity<ProductCompany>()
                        .HasOne(p => p.Product)
                        .WithMany(e => e.ProductsCompanies)
                        .HasForeignKey(k => k.ProductID);

                    modelBuilder.Entity<ProductCompany>()
                    .HasOne(e => e.Company)
                    .WithMany(e => e.ProductsCompanies)
                    .HasForeignKey(k => k.CompanyID);
            #endregion

            #region Company-Campaign

                    modelBuilder.Entity<CompanyCampaign>()
                        .HasOne<Campaign>(e => e.Campaign)
                        .WithMany(e => e.CompaniesCampaigns)
                        .HasForeignKey(e => e.CampaignID);

                    modelBuilder.Entity<CompanyCampaign>()
                        .HasOne<Company>(e => e.Company)
                        .WithMany(e => e.CompaniesCampaigns)
                        .HasForeignKey(e => e.CompanyID);

                    modelBuilder.Entity<CompanyCampaign>()
                                .HasOne<Product>(e => e.Product)
                                .WithMany(e => e.CompaniesCampaigns)
                                .HasForeignKey(e => e.ProductID);

            #endregion


            base.OnModelCreating(modelBuilder);

            #region Product Entity shadow propperties

            modelBuilder.Entity<Product>().Property<DateTime>("CreatedDTTM").HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Product>().Property<DateTime>("UpdatedDTTM").HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Product>().Property<string>("CreatedBy");
            modelBuilder.Entity<Product>().Property<string>("UpdatedBy");

            modelBuilder.Entity<Product>().Property<string>("RequestedIP");

            #endregion


            #region Campaign Entity shadow propperties

            modelBuilder.Entity<Campaign>().Property<DateTime>("CreatedDTTM").HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Campaign>().Property<DateTime>("UpdatedDTTM").HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Campaign>().Property<string>("CreatedBy");
            modelBuilder.Entity<Campaign>().Property<string>("UpdatedBy");

            modelBuilder.Entity<Campaign>().Property<string>("RequestedIP");

            #endregion


            #region Company Entity shadow propperties

            modelBuilder.Entity<Company>().Property<DateTime>("CreatedDTTM").HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Company>().Property<DateTime>("UpdatedDTTM").HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Company>().Property<string>("CreatedBy");
            modelBuilder.Entity<Company>().Property<string>("UpdatedBy");

            modelBuilder.Entity<Company>().Property<string>("RequestedIP");
            #endregion
        }


        #region Entity datasets
        public DbSet<Product> Products { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Company> Companies { get; set; }

        //public DbSet<ProductCampaign> ProductsCampaigns { get; set; }
        public DbSet<CompanyCampaign> CompaniesCampaigns { get; set; }
        public DbSet<ProductCompany> ProductsCompanies { get; set; }

        #endregion

    }
    #endregion
}
