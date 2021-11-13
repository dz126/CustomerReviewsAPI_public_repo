using CustomerReviewsAPI_.Controllers;
using Microsoft.EntityFrameworkCore;
using System;

namespace CustomerReviewsAPI_.Models
{
    public class OnBeforeSave : IOnBeforeSave
    {
        #region DBContext

        private readonly ApplicationDBContext context;
        
        #endregion

        public OnBeforeSave(ApplicationDBContext _context)
        {
            context = _context;
        }

        #region Update before save
        public void BeforeSave(Type type) 
        {

            context.ChangeTracker.DetectChanges();

            #region Update Company

           

            if (type.Equals(typeof(Company)))
            {
                var customerEntries = context.ChangeTracker.Entries<Company>();


                foreach (var entityEntry in customerEntries)
                {

                    if (entityEntry.State.Equals(EntityState.Added))
                    {

                        entityEntry.Property("CreatedDTTM").CurrentValue = DateTime.Now;

                        entityEntry.Property("CreatedBy").CurrentValue = "Admin";

                    }
                    else if (entityEntry.State.Equals(EntityState.Modified))
                    {
                        entityEntry.Property("UpdatedDTTM").CurrentValue = DateTime.Now;
                        entityEntry.Property("UpdatedBy").CurrentValue = "Admin";
                    }

                }

            }
            #endregion

            #region Update Campaign

            
            else if (type.Equals(typeof(Campaign)))
            {
                var customerEntries = context.ChangeTracker.Entries<Campaign>();


                foreach (var entityEntry in customerEntries)
                {

                    if (entityEntry.State.Equals(EntityState.Added))
                    {

                        entityEntry.Property("CreatedDTTM").CurrentValue = DateTime.Now;

                        entityEntry.Property("CreatedBy").CurrentValue = "Admin";

                    }
                    else if (entityEntry.State.Equals(EntityState.Modified))
                    {
                        entityEntry.Property("UpdatedDTTM").CurrentValue = DateTime.Now;
                        entityEntry.Property("UpdatedBy").CurrentValue = "Admin";
                    }

                }

            }
            #endregion

            #region Update Product


            else if (type.Equals(typeof(Product)))
            {
                var customerEntries = context.ChangeTracker.Entries<Product>();


                foreach (var entityEntry in customerEntries)
                {

                    if (entityEntry.State.Equals(EntityState.Added))
                    {

                        entityEntry.Property("CreatedDTTM").CurrentValue = DateTime.Now;

                        entityEntry.Property("CreatedBy").CurrentValue = "Admin";

                    }
                    else if (entityEntry.State.Equals(EntityState.Modified))
                    {
                        entityEntry.Property("UpdatedDTTM").CurrentValue = DateTime.Now;
                        entityEntry.Property("UpdatedBy").CurrentValue = "Admin";
                    }

                }

            }
            #endregion

        }

        #endregion
    }
}
