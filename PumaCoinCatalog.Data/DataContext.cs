﻿using PumaCoinCatalog.Data.Configuration;
using PumaCoinCatalog.Models;
using PumaCoinCatalog.Models.UsaCoinBook;
using PumaCoinCatalog.Models.UsaCoinBook.Checklists;
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

        public DbSet<CbCountry> CbCountries { get; set; }
        public DbSet<CbDenomination> CbDenominations { get; set; }
        public DbSet<CbVariety> CbVarieties { get; set; }
        public DbSet<CbType> CbTypes { get; set; }
        public DbSet<CbCoin> CbCoins { get; set; }

        public DbSet<CbCollection> CbCollections { get; set; }
        public DbSet<CbChecklist> CbChecklists { get; set; }
        public DbSet<CbChecklistCoin> CbChecklistCoins { get; set; }

        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<ChecklistCoin> ChecklistCoins { get; set; }

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

            modelBuilder.Configurations.Add(new CbCountryConfig());
            modelBuilder.Configurations.Add(new CbDenominationConfig());
            modelBuilder.Configurations.Add(new CbVarietyConfig());
            modelBuilder.Configurations.Add(new CbTypeConfig());
            modelBuilder.Configurations.Add(new CbCoinConfig());

            modelBuilder.Configurations.Add(new CbCollectionConfig());
            modelBuilder.Configurations.Add(new CbChecklistConfig());
            modelBuilder.Configurations.Add(new CbChecklistCoinConfig());

            modelBuilder.Configurations.Add(new ChecklistConfig());
            modelBuilder.Configurations.Add(new ChecklistCoinConfig());
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
