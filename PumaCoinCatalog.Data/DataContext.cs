using PumaCoinCatalog.Data.Configuration;
using PumaCoinCatalog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumaCoinCatalog.Data
{
    public class DataContext : DbContext
    {
        #region Initialization

        public DataContext()
            : base("PumaCoinCatalog")
        {
        }

        public static void InitializeDatabase()
        {
            var initializeMigrations = new MigrateDatabaseToLatestVersion<DataContext, DbConfiguration>();
            initializeMigrations.InitializeDatabase(new DataContext());
        }

        #endregion Initialization

        #region Models and Configuration
        
        public DbSet<ScrapeCoinCollection> ScrapeCoinCollections { get; set; }
        public DbSet<ScrapeCoinCategory> ScrapeCoinCategories { get; set; }
        public DbSet<ScrapeCoinType> ScrapeCoinTypes { get; set; }
        public DbSet<ScrapeCoin> ScrapeCoins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            

            // conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // configurations            
            modelBuilder.Configurations.Add(new ScrapeCoinCollectionConfig());
            modelBuilder.Configurations.Add(new ScrapeCoinCategoryConfig());
            modelBuilder.Configurations.Add(new ScrapeCoinTypeConfig());
            modelBuilder.Configurations.Add(new ScrapeCoinConfig());
        }

        #endregion Models and Configuration

        #region Save and Update Entities

        public override int SaveChanges()
        {            
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                        .SelectMany(x => x.ValidationErrors)
                                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
        
        #endregion Save and Update Entities
    }
}
