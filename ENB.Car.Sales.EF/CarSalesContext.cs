using ENB.Car.Sales.EF.ConfigurationEntity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ENB.Car.Sales.Entities;
using ENB.Car.Sales.Infrastructure;

namespace ENB.Car.Sales.EF
{
    /// <summary>
    /// This is the main DbContext to work with data in the database.
    /// </summary>
    public class CarSalesContext : IdentityDbContext<ApplicationUser>
    {
        public CarSalesContext(DbContextOptions<CarSalesContext> options)
               : base(options)
        {

        }

        public DbSet<Customer>? Customers { get; set; }
        public DbSet<CarForSale>? CarForSales { get; set; }





        /// <summary>
        /// Hooks into the Save process to get a last-minute chance to look at the entities and change them. Also intercepts exceptions and 
        /// wraps them in a new Exception type.
        /// </summary>
        /// <returns>The number of affected rows.</returns>


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {



            try
            {
                var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
                foreach (EntityEntry item in modified)
                {
                    var changedOrAddedItem = item.Entity as IDateTracking;
                    if (changedOrAddedItem != null)
                    {
                        if (item.State == EntityState.Added)
                        {
                            changedOrAddedItem.DateCreated = DateTime.Now;
                        }
                        changedOrAddedItem.DateModified = DateTime.Now;
                    }
                    var valProvider = new ValidationDbContextServiceProvider(this);
                    var validationContext = new ValidationContext(item, valProvider, null);
                    Validator.ValidateObject(item, validationContext);
                }
                // return base.SaveChanges();
            }
            catch (Exception)
            {

                // throw new ModelValidationException(result.ToString(), entityException, allErrors);
                //var exStatus = SaveChangesExtensions.SaveChangesExceptionHandler(e, this);
                //if (exStatus == null) throw;       //error wasn't handled, so rethrow
                //status.CombineStatuses(exStatus);
            }
            return base.SaveChangesAsync(cancellationToken);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
